using Loscate.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.Models
{
    [TestClass]
    public class NotificationEventArgsTests
    {
        private const string testTitle = "title";
        private const string testMessage = "message";
        
        [TestMethod]
        public void WriteReadTest()
        {
            var notificationEventArgs = new NotificationEventArgs()
            {
                Message = testMessage,
                Title = testTitle
            };
            
            Assert.AreEqual(notificationEventArgs.Message, testMessage);
            Assert.AreEqual(notificationEventArgs.Title, testTitle);
        }
    }
}