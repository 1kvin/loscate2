using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loscate.Site.Controllers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace Loscate.Site.Controllers.User.Tests
{
    [TestClass()]
    public class EditPhotoControllerTests
    {
        const int testUserId = 1;
        const string testUserUid = "uid";


        [TestMethod]
        public void PostLimitPhotoTest()

        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid };

            var users = new List<FirebaseUser>()
            {
                testUser
            }.AsQueryable();


            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser);

            var controller = new EditPhotoController(mockContextDb.Object, mockUserService.Object);

            var photo = new string('a', 1024 * 1024 * 21);
            var result = controller.Post(photo);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "LIMIT");
        }

        [TestMethod]
        public void PostCorrectPhotoTest()

        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid };

            var users = new List<FirebaseUser>()
            {
                testUser
            }.AsQueryable();




            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser);

            var controller = new EditPhotoController(mockContextDb.Object, mockUserService.Object);

            var photo = new string('a', 1024 * 1024 * 19);
            var result = controller.Post(photo);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }
    }
}