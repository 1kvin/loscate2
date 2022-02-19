using System;
using Loscate.App.Models;
using Loscate.DTO.Firebase;
using Loscate.DTO.Social;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Message = Loscate.DTO.Social.Message;

namespace Loscate.App.UnitTests.Models
{
    [TestClass]
    public class XamarinDialogTest
    {
        private const string testUserUid = "uid";
        private const string testUserName = "name";
        private const string testUserEMail = "email";
        private const string testUserPictureUrl = "pic";
        private const string testUserPhotoBase64 = "photo";

        private const string testMessageText = "text";

        [TestMethod]
        public void ConvertTest()
        {
            var companion = new FirebaseUser(testUserUid, testUserName, testUserEMail, testUserPictureUrl,
                testUserPhotoBase64);
            
            var dialog = new Dialog()
            {
                Companion = companion,
                LastMessage = new Message() { Text = testMessageText, Time = DateTime.UtcNow, SendUser = companion }
            };

            var xamarinDialog = new XamarinDialog(dialog);
            
            Assert.AreEqual(xamarinDialog.Companion.Name, testUserName);
            Assert.AreEqual(xamarinDialog.Companion.EMail, testUserEMail);
            Assert.AreEqual(xamarinDialog.Companion.PictureUrl, testUserPictureUrl);
            Assert.AreEqual(xamarinDialog.Companion.UID, testUserUid);
            
            Assert.AreEqual(xamarinDialog.LastMessage.Text, testMessageText);
        }
    }
}