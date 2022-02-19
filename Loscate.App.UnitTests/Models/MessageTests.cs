using Loscate.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.Models
{
    [TestClass]
    public class MessageTests
    {
        private const string testText = "text";
        private const string testUser = "user";
        
        [TestMethod]
        public void WriteReadTest()
        {
            var message = new Message
            {
                Text = testText,
                User = testUser
            };
            
            Assert.AreEqual(message.Text, testText);
            Assert.AreEqual(message.User, testUser);
        }
    }
}