using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loscate.Site.Controllers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Loscate.Site.DbContext;
using Loscate.Site.Repository;
using Loscate.Site.Services;
using System.Security.Claims;

namespace Loscate.Site.Controllers.User.Tests
{
    [TestClass()]
    public class GetFirebaseUserControllerTests
    {
        private const int testUserId = 1;
        private const string testUserUid = "uid";
        private const string testUserEmail = "email";
        private const string testUserName = "name";
        private const string testUserPhoto = "photo";
        private const string testUserPhotoUrl = "https://google.ru";
        [TestMethod()]
        public void GetTest()
        {
            var testUser = new DbContext.FirebaseUser()
            {
                Id = testUserId,
                Email = testUserEmail,
                Name = testUserName,
                Uid = testUserUid,
                PictureUrl = testUserPhotoUrl,
                PhotoBase64 = testUserPhoto,
            };


            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser);

            var controller = new GetFirebaseUserController(mockUserService.Object);
            var result = controller.Get();

            Assert.AreEqual(result.Name, testUserName);
            Assert.AreEqual(result.EMail, testUserEmail);
            Assert.AreEqual(result.PhotoBase64, testUserPhoto);
            Assert.AreEqual(result.UID, testUserUid);
            Assert.AreEqual(result.PictureUrl, testUserPhotoUrl);
        }
    }
}