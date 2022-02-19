using Loscate.App.Models;
using Loscate.App.Services;
using Loscate.DTO.Firebase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.Models
{
    [TestClass]
    public class XamarinUserTests
    {
        private const string testUserUid = "uid";
        private const string testUserName = "name";
        private const string testUserEMail = "email";
        private const string testUserPictureUrl = "https://en.wikipedia.org/wiki/Main_Page";
        private const string testUserPhotoBase64 = "photo";


        [TestMethod]
        public void ConvertTest()
        {
            var user = new FirebaseUser(testUserUid, testUserName, testUserEMail, testUserPictureUrl,
                testUserPhotoBase64);

            var xamarinUser = new XamarinUser(user);
            
            Assert.AreEqual(xamarinUser.Name, testUserName);
            Assert.AreEqual(xamarinUser.UID, testUserUid);
            Assert.AreEqual(xamarinUser.EMail, testUserEMail);
            Assert.AreEqual(xamarinUser.PhotoBase64, testUserPhotoBase64);
        }

        [TestMethod]
        public void PhotoFromWebTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            var user = new FirebaseUser(testUserUid, testUserName, testUserEMail, testUserPictureUrl, string.Empty);
            var xamarinUser = new XamarinUser(user);
            
            Assert.IsFalse(xamarinUser.UserImage.IsEmpty);
        }
    }
}