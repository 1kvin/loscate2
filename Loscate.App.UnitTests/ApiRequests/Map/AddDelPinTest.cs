using System.Linq;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Loscate.DTO.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loscate.App.UnitTests.ApiRequests.Map
{
    [TestClass]
    public class AddDelPinTest
    {
        private readonly FakeAuthenticator authenticator;

        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        private const double testPinLatitude = 0;
        private const double testPinLongitude = 0;
        private const string testPinPhotoBase64 = "";
        private const string testPinFullName = "FullName";
        private const string testPinShortName = "ShortName";
        
        public AddDelPinTest()
        {
            authenticator = new FakeAuthenticator();
        }
        
        [TestMethod]
        public async Task AddDelTest()
        {
            var testPin = new Pin()
            {
                Latitude = testPinLatitude,
                Longitude = testPinLongitude,
                FullName = testPinFullName,
                PhotoBase64 = testPinPhotoBase64,
                ShortName = testPinShortName,
                UserUID = testPinUserUID
            };
            
            var addResult = await MapRequests.AddPin(authenticator, testPin);
            
            Assert.AreEqual(addResult, "OK");
            
            var pins = await MapRequests.GetPins(authenticator);
            var myPin = pins.Where(p => p.UserUID == testPinUserUID).ToList();
            
            Assert.IsTrue(myPin.Count > 0);
            
            var delResult = await MapRequests.DelPin(authenticator, myPin[0].Id);
            
            Assert.AreEqual(delResult, "OK");
        }
    }
}