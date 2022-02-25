using System;
using System.Collections.Generic;
using System.Linq;
using Loscate.Site.Controllers.Map;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Loscate.Site.UnitTests.BusinessLogic.Map
{
    [TestClass]
    public class SearchPinTests
    {
        const int testUserId = 1;
        const string testUserUid = "uid";

        const double testPinLatitude = 1;
        const double testPinLongitude = 2;
        const string testPinPhoto = "photo";
        const string testPinFullName = "FullName";
        const string testPinShortName = "ShortName";


        [TestMethod]
        public void PartOfTextTest()
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
            
            var result = GetResult(testPinFullName[..3], pins, users);
            
            Assert.AreEqual(result.Count, 1);
        }
        
        
        [TestMethod]
        public void EmptyResultTest()
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
            
            var result = GetResult(Guid.NewGuid().ToString(), pins, users);
            
            Assert.AreEqual(result.Count, 0);
        }
        
        [TestMethod]
        public void SimpleFilterTest()
        {
            const string testAnotherPinFullName = "Pin2FN";
            const string testAnotherPinShortName = "Pin2SN";
            
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
                },
                new()
                {
                    Latitude = testPinLatitude,
                    Longitude = testPinLongitude,
                    Photo = testPinPhoto,
                    FullName = testAnotherPinFullName,
                    ShortName = testAnotherPinShortName,
                    User = testUser,
                    UserId = testUser.Id
                }
            }.AsQueryable();
            
            var result = GetResult(testPinFullName, pins, users);
            
            Assert.AreEqual(result.Count, 1);
        }
        
        
        [TestMethod]
        public void EmptyFilterTest()
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
                },
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
            
            var result = GetResult(string.Empty, pins, users);
            
            Assert.AreEqual(result.Count, 2);
        }
        
        private List<DTO.Map.Pin> GetResult(string filter, IQueryable<Pin> pins, IQueryable<FirebaseUser> users)
        {
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

            var mockContext = new Mock<ILoscateDbRepository>();
            mockContext.Setup(c => c.Pins).Returns(pinsMockSet.Object);
            mockContext.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var controller = new SearchPinController(mockContext.Object);
            return controller.Get(filter);
        }
    }
}