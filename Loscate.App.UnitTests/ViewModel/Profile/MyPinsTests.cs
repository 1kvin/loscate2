using Loscate.App.Map;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.ViewModel.Profile
{
    [TestClass]
    public class MyPinsTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        private const string testPinPhoto = "photo";
        private const string testPinFullName = "FullName";
        private const string testPinShortName = "ShortName";
        
        [TestMethod]
        public void MyPins()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
           

            var viewModel = new MyPinsViewModel();
            
            viewModel.LoadItemsCommand.Execute(null);

            var pin = new CustomPin()
            {
                ShortName = testPinShortName,
                FullName = testPinFullName,
                UserUID = testPinUserUID,
                PinId = 1,
                Photo = testPinPhoto
            };

            viewModel.SelectedItem = pin;
        }
    }
}