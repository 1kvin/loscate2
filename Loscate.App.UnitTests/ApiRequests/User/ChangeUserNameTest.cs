using System;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.User
{
    [TestClass]
    public class ChangeUserNameTest
    {
        private readonly FakeAuthenticator authenticator;
        private readonly string newName;

        public ChangeUserNameTest()
        {
            authenticator = new FakeAuthenticator();
            newName = Guid.NewGuid().ToString().Substring(0, 20);
        }
        
        [TestMethod]
        public async Task ChangeUserName()
        {
            var result = await UserRequests.ChangeUserName(authenticator, newName);
            Assert.AreEqual(result, "OK");
            
            var user = await UserRequests.GetUser(authenticator);
            
            Assert.AreEqual(user.Name, newName);
        }
    }
}