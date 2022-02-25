using Loscate.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.Models
{
    [TestClass]
    public class ItemTests
    {
        private const string testId = "id";
        private const string testText = "text";
        private const string testDescription = "description";
        
        [TestMethod]
        public void WriteReadTest()
        {
            var item = new Item()
            {
                Description = testDescription,
                Id = testId,
                Text = testText
            };
            
            Assert.AreEqual(item.Description, testDescription);
            Assert.AreEqual(item.Id, testId);
            Assert.AreEqual(item.Text, testText);
        }
    }
}