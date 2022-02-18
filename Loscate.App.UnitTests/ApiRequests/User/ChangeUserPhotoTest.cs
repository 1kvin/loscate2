using System;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.User
{
    [TestClass]
    public class ChangeUserPhotoTest
    {
        private readonly FakeAuthenticator authenticator;
        private readonly string newPhoto; 

        public ChangeUserPhotoTest()
        {
            authenticator = new FakeAuthenticator();
            newPhoto = Guid.NewGuid().ToString();
        }

        [TestMethod]
        public async Task ChangeUserPhoto()
        {
            var result = await UserRequests.ChangeUserPhoto(authenticator, newPhoto);
            Assert.AreEqual(result, "OK");
            
            var user = await UserRequests.GetUser(authenticator);
            
            Assert.AreEqual(user.PhotoBase64, newPhoto);
        }
    }
}