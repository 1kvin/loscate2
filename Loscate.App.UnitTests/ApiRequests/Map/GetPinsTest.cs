using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.Map
{
    [TestClass]
    public class GetPinsTest
    {
        private readonly FakeAuthenticator authenticator;

        public GetPinsTest()
        {
            authenticator = new FakeAuthenticator();
        }

        [TestMethod]
        public async Task CommonTest()
        {
            var pins = await MapRequests.GetPins(authenticator);
            Assert.IsTrue(pins.Count > 0);
        }
    }
}