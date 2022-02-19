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
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText = Guid.NewGuid().ToString(),
                FullText = Guid.NewGuid().ToString(),
                position = new Position(-1, -1)
            };
            viewModel.AddPinCommand.Execute(null);

            var authenticator = new FakeAuthenticator();
            var pins = await MapRequests.GetPins(authenticator);
            var myPin = pins.Where(p => p.UserUID == testPinUserUID).ToList();
            myPin.ForEach(p => MapRequests.DelPin(authenticator, p.Id));
        }


        [TestMethod]
        public void NoPhotoLimit()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = string.Empty,
                ShortText = Guid.NewGuid().ToString(),
                FullText = Guid.NewGuid().ToString(),
                position = new Position(-1, -1)
            };

            Assert.AreEqual(viewModel.ValidateData(), "Выберите фото!");
        }
        
        [TestMethod]
        public void EmptyShortText()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText = string.Empty,
                FullText = Guid.NewGuid().ToString(),
                position = new Position(-1, -1)
            };

            Assert.AreEqual(viewModel.ValidateData(), "Краткое описание не может быть пустым!");
        }
        
        [TestMethod]
        public void ShortLenghtLimitText()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText = "123",
                FullText = Guid.NewGuid().ToString(),
                position = new Position(-1, -1)
            };

            Assert.AreEqual(viewModel.ValidateData(), "Краткое описание не может быть меньше 10 символов!");
        }
        
        [TestMethod]
        public void EmptyFullTextText()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText =  Guid.NewGuid().ToString(),
                FullText = string.Empty,
                position = new Position(-1, -1)
            };

            Assert.AreEqual(viewModel.ValidateData(), "Подробное описание не может быть пустым!");
        }
        
        [TestMethod]
        public void FullTextLenghtLimitText()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText =  Guid.NewGuid().ToString(),
                FullText = "123",
                position = new Position(-1, -1)
            };

            Assert.AreEqual(viewModel.ValidateData(), "Подробное описание не может быть меньше 20 символов!");
        }
        
        [TestMethod]
        public void LocationNotSetText()
        {
            var viewModel = new AddPinViewModel
            {
                base64photo = Guid.NewGuid().ToString(),
                ShortText =  Guid.NewGuid().ToString(),
                FullText = Guid.NewGuid().ToString()
            };

            Assert.AreEqual(viewModel.ValidateData(), "Выберите геометку!");
        }
    }
}