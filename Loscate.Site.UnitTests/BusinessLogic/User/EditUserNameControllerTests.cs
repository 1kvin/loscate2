using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loscate.Site.Controllers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Loscate.Site.DbContext;
using Microsoft.EntityFrameworkCore;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using System.Security.Claims;

namespace Loscate.Site.Controllers.User.Tests
{
    [TestClass()]
    public class EditUserNameControllerTests
    {
        const int testUserId = 1;
        const string testUserUid = "uid";
        const string testUserName = "name";

        [TestMethod()]
        public void GetLimitTooLongTest()
        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid, Name = testUserName };

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

            var controller = new EditUserNameController(mockContextDb.Object, mockUserService.Object);

            var name = new string('a', 31);
            var result = controller.Get(name);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "LIMIT");
        }

        [TestMethod()]
        public void GetLimitTooShortTest()
        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid, Name = testUserName };

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

            var controller = new EditUserNameController(mockContextDb.Object, mockUserService.Object);

            var name = new string('a', 9);
            var result = controller.Get(name);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "LIMIT");
        }

        [TestMethod()]
        public void GetOkTest()
        {
            var testUser = new FirebaseUser { Id = testUserId, Uid = testUserUid, Name = testUserName };

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

            var controller = new EditUserNameController(mockContextDb.Object, mockUserService.Object);

            var name = new string('a', 12);
            var result = controller.Get(name);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }
    }
}