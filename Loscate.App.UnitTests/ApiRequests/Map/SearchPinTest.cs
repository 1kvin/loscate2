using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.Map
{
    [TestClass]
    public class SearchPinTest
    {
        private readonly FakeAuthenticator authenticator;

        public SearchPinTest()
        {
            authenticator = new FakeAuthenticator();
        }

        [TestMethod]
        public async Task EmptyFilterTest()
        {
            var pins = await MapRequests.SearchPin(authenticator, "");
            Assert.IsTrue(pins.Count > 0);
        }
    }
}