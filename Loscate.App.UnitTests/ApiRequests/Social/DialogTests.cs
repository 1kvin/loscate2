using System.Threading.Tasks;
using Loscate.App.ApiRequests.Social.Dialog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.Social
{
    [TestClass]
    public class DialogTests
    {
        private readonly FakeAuthenticator authenticator;
        
        public DialogTests()
        {
            authenticator = new FakeAuthenticator();
        }

        [TestMethod]
        public async Task ExistSelfDialogTest()
        {
            var dialogs = await DialogRequests.GetUserDialogs(authenticator);
            
            Assert.IsTrue(dialogs.Count > 0);
        }
    }
}