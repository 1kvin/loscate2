using Loscate.App.Map;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Loscate.App.UnitTests.ViewModel.Pin
{
    [TestClass]
    public class SearchPinTests
    {
        private const double testPinLatitude = 0;
        private const double testPinLongitude = 0;
        private const string testPinPhotoBase64 = "";
        private const string testPinFullName = "FullName";
        private const string testPinShortName = "ShortName";
        public SearchPinTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            MockForms.Init();
            Application.Current = new App();
        }

        [TestMethod]
        public void LoadDialogsTest()
        {
            var viewModel = new SearchPinViewModel();
            viewModel.SearchText = string.Empty;
            viewModel.LoadItemsCommand.Execute(null);
        }
        
        [TestMethod]
        public void SelectItemTest()
        {
            var viewModel = new SearchPinViewModel();
            viewModel.SelectedItem = new CustomPin();
        }
    }
}