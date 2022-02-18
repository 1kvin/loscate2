using System;
using System.Linq;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Social.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.Social
{
    [TestClass]
    public class SendMessageTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        private readonly string testMsg;
        
        private readonly FakeAuthenticator authenticator;

        public SendMessageTests()
        {
            authenticator = new FakeAuthenticator();
            testMsg = Guid.NewGuid().ToString();
        }

        [TestMethod]
        public async Task SendMessageToYourself()
        {
            var sendMessageResult = await MessageRequests.SendMessage(authenticator, testPinUserUID, testMsg);
            Assert.AreEqual(sendMessageResult, "OK");
            
            var msgs = await MessageRequests.GetUserMessage(authenticator, testPinUserUID);
            var sendMessage = msgs.SingleOrDefault(m => m.Text == testMsg);
            
            Assert.IsNotNull(sendMessage);
        }
    }
}