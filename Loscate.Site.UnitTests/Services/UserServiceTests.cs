using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Loscate.Site.UnitTests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private const string testUserName = "Artem";
        private const string testUserUid = "uid";

        private const string testUserGoodPicture =
            "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png";

        private const string testUserFirebaseProfile = "{  \"identities\" :   {    \"email\" : [\"myEmail\"]  }}";


        [TestMethod]
        public void GetFirebaseUserFromClaimsIdentityTest()
        {
            var claims = new List<Claim>()
            {
                new("name", testUserName),
                new("user_id", testUserUid),
                new("picture", testUserGoodPicture),
                new("firebase", testUserFirebaseProfile),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var testUser = new FirebaseUser { 
                Id = 1, 
                Uid = testUserUid,
                Name = testUserName,
                PictureUrl = testUserGoodPicture
            };

            var users = new List<FirebaseUser>()
            {
                testUser
            }.AsQueryable();

            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<ILoscateDbRepository>();
            mockContext.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var userService = new UserService(mockContext.Object);
            var dbUser = userService.GetDbUser(claimsPrincipal);

            Assert.AreEqual(dbUser.Name, testUserName);
            Assert.AreEqual(dbUser.Uid, testUserUid);
            Assert.AreEqual(dbUser.PictureUrl, testUserGoodPicture);
        }
    }
}