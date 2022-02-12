using Loscate.Site.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.Site.UnitTests.Utilities.DtoConvert
{
    [TestClass]
    public class ConvertFirebaseUserTest
    {
        private const int testUserId = 1;
        private const string testUserUid = "uid";
        private const string testUserEmail = "email";
        private const string testUserName = "name";
        private const string testUserPhoto = "photo";
        private const string testUserPhotoUrl = "https://google.ru";
    
        [TestMethod]
        public void DbFirebaseUserToDtoConvert()
        {
            var dbUser = new DbContext.FirebaseUser()
            {
                Id = testUserId,
                Email = testUserEmail,
                Name = testUserName,
                Uid = testUserUid,
                PictureUrl = testUserPhotoUrl,
                PhotoBase64 = testUserPhoto,
            };

            var dtoUser = dbUser.ConvertToDto();
        
            Assert.AreEqual(dtoUser.Name, testUserName);
            Assert.AreEqual(dtoUser.EMail, testUserEmail);
            Assert.AreEqual(dtoUser.PhotoBase64, testUserPhoto);
            Assert.AreEqual(dtoUser.UID, testUserUid);
            Assert.AreEqual(dtoUser.PictureUrl, testUserPhotoUrl);
        }
    
        [TestMethod]
        public void DtoFirebaseUserToDbConvert()
        {
            var dtoUser = new DTO.Firebase.FirebaseUser()
            {
                Name = testUserName,
                EMail = testUserEmail,
                UID = testUserUid,
                PictureUrl = testUserPhotoUrl,
                PhotoBase64 = testUserPhoto,
            };

            var dbUser = dtoUser.ConvertToDb();
        
            Assert.AreEqual(dbUser.Name, testUserName);
            Assert.AreEqual(dbUser.Email, testUserEmail);
            Assert.AreEqual(dbUser.PhotoBase64, testUserPhoto);
            Assert.AreEqual(dbUser.Uid, testUserUid);
            Assert.AreEqual(dbUser.PictureUrl, testUserPhotoUrl);
        }
    }
}