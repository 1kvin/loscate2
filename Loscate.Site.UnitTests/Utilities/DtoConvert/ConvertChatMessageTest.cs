using System;
using Loscate.Site.DbContext;
using Loscate.Site.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.Site.UnitTests.Utilities.DtoConvert
{
    [TestClass]
    public class ConvertChatMessageTest
    {
        private const string testUserUid = "uid";
        private const string testMessageText = "text";
    
        private readonly DateTime testMessageTime;
        private readonly FirebaseUser testUser;

        public ConvertChatMessageTest()
        {
            testUser = new FirebaseUser { Uid = testUserUid };
            testMessageTime = DateTime.UtcNow;
        }
    
        [TestMethod]
        public void DbChatMessageToDtoConvert()
        {
            var dbChatMessage = new ChatMessage()
            {   
                Text = testMessageText,
                Time = testMessageTime,
                SendUser = testUser,
                SendUserId = testUser.Id
            };

            var dtoChatMessage = dbChatMessage.ConvertToDto();
        
            Assert.IsNotNull(dtoChatMessage);
            Assert.AreEqual(dtoChatMessage.Text, testMessageText);
            Assert.AreEqual(dtoChatMessage.Time, testMessageTime);
            Assert.AreEqual(dtoChatMessage.SendUser.UID, testUserUid);
        }

        [TestMethod]
        public void NullChatMessage()
        {
            var dbChatMessage = (ChatMessage)null!;
            var dtoChatMessage = dbChatMessage.ConvertToDto();
            Assert.IsNull(dtoChatMessage);
        }
    }
}