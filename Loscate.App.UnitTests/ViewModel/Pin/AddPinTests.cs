using System;
using System.Linq;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Mocks;

namespace Loscate.App.UnitTests.ViewModel.Pin
{
    [TestClass]
    public class AddPinTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        
        public AddPinTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            MockForms.Init();
            Application.Current = new App();
        }

        [TestMethod]
        public async Task AddPin()
        {
            var viewModel = new AddPinViewModel();
            viewModel.base64photo = Guid.NewGuid().ToString();
            viewModel.ShortText = Guid.NewGuid().ToString();
            viewModel.FullText = Guid.NewGuid().ToString();
            viewModel.position = new Position(-1, -1);
            viewModel.AddPinCommand.Execute(null);

            var authenticator = new FakeAuthenticator();
            var pins = await MapRequests.GetPins(authenticator);
            var myPin = pins.Where(p => p.UserUID == testPinUserUID).ToList();
            myPin.ForEach(p => MapRequests.DelPin(authenticator, p.Id));
        }
        
        //TODO limits
    }
}