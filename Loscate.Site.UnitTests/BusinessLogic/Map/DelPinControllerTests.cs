using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Loscate.Site.Controllers.Map.Tests
{
    [TestClass]
    public class DelPinControllerTests
    {
        const int testUserId = 1;
        const string testUserUid = "uid";
        const double testPinLatitude = 1;
        const double testPinLongitude = 2;
        const string testPinPhoto = "photo";
        const string testPinFullName = "FullName";
        const string testPinShortName = "ShortName";


        [TestMethod]
        public void DelByCorrectIdTest()

        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid };

            var users = new List<FirebaseUser>()
            {
                testUser
            }.AsQueryable();

            var pins = new List<Pin>()
            {
                new()
                {
                    Latitude = testPinLatitude,
                    Longitude = testPinLongitude,
                    Photo = testPinPhoto,
                    FullName = testPinFullName,
                    ShortName = testPinShortName,
                    User = testUser,
                    UserId = testUser.Id
                }
            }.AsQueryable();

            var pinsMockSet = new Mock<DbSet<Pin>>();
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.Provider).Returns(pins.Provider);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.Expression).Returns(pins.Expression);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.ElementType).Returns(pins.ElementType);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.GetEnumerator()).Returns(pins.GetEnumerator());

            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.Pins).Returns(pinsMockSet.Object);
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser);

            var controller = new DelPinController(mockContextDb.Object, mockUserService.Object);
            var result = controller.Get(0);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }

        [TestMethod]
        public void DelByWrongIdTest()

        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid };

            var users = new List<FirebaseUser>()
            {
                testUser
            }.AsQueryable();

            var pins = new List<Pin>()
            {
                new()
                {
                    Latitude = testPinLatitude,
                    Longitude = testPinLongitude,
                    Photo = testPinPhoto,
                    FullName = testPinFullName,
                    ShortName = testPinShortName,
                    User = testUser,
                    UserId = testUser.Id
                }
            }.AsQueryable();

            var pinsMockSet = new Mock<DbSet<Pin>>();
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.Provider).Returns(pins.Provider);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.Expression).Returns(pins.Expression);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.ElementType).Returns(pins.ElementType);
            pinsMockSet.As<IQueryable<Pin>>().Setup(m => m.GetEnumerator()).Returns(pins.GetEnumerator());

            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.Pins).Returns(pinsMockSet.Object);
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser);

            var controller = new DelPinController(mockContextDb.Object, mockUserService.Object);
            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, "OK");
        }

    }
}