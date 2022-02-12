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
    public class GetPinsTest
    {
        [TestMethod]
        public void CommonTest()
        {
            const int testUserId = 1;
            const string testUserUid = "uid";

            const double testPinLatitude = 1;
            const double testPinLongitude = 2;
            const string testPinPhoto = "photo";
            const string testPinFullName = "FullName";
            const string testPinShortName = "ShortName";

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

            var mockContext = new Mock<ILoscateDbRepository>();
            mockContext.Setup(c => c.Pins).Returns(pinsMockSet.Object);
            mockContext.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var controller = new GetPinsController(mockContext.Object);
            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);

            var testPin = result.Single();

            Assert.AreEqual(testPin.Latitude, testPinLatitude);
            Assert.AreEqual(testPin.Longitude, testPinLongitude);
            Assert.AreEqual(testPin.PhotoBase64, testPinPhoto);
            Assert.AreEqual(testPin.FullName, testPinFullName);
            Assert.AreEqual(testPin.ShortName, testPinShortName);
        
            Assert.AreEqual(testPin.UserUID, testUserUid);
        }
    }
}