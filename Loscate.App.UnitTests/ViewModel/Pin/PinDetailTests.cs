using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Loscate.App.UnitTests.ViewModel.Pin
{
    [TestClass]
    public class PinDetailTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        
        public PinDetailTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            MockForms.Init();
            Application.Current = new App();
        }
        
        [TestMethod]
        public void BackTest()
        {
            var viewModel = new PinDetailViewModel();
            viewModel.BackCommand.Execute(null);
        }
        
        [TestMethod]
        public void DeletePinTest()
        {
            var viewModel = new PinDetailViewModel
            {
                PinId = -1
            };
            viewModel.DeletePinCommand.Execute(null);
        }
        
        [TestMethod]
        public void WriteMessageTest()
        {
            var viewModel = new PinDetailViewModel
            {
               UserUID = testPinUserUID
            };
            
            viewModel.WriteMessageCommand.Execute(null);
        }
    }
}