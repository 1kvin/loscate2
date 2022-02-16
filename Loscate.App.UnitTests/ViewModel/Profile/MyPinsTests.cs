using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.ViewModel.Profile
{
    [TestClass]
    public class MyPinsTests
    {
        [TestMethod]
        public void Test()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
           

            var viewModel = new MyProfileViewModel();
            

            viewModel.EditAccountCommand.Execute(null);
            viewModel.OpenMyPinsCommand.Execute(null);
            viewModel.SignOutCommand.Execute(null);
        }
    }
}