using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Loscate.App.UnitTests.ViewModel.Login
{
    [TestClass]
    public class LoginTests
    {
        public LoginTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            MockForms.Init();
            Application.Current = new App();
        }
        
        [TestMethod]
        public void LoginTest()
        {
            var viewModel = new LoginViewModel();
            viewModel.GoogleSignInCommand.Execute(null);
            
            var firebaseAuth = (FakeAuthenticator)DependencyService.Get<IFirebaseAuthenticator>();
            Assert.IsTrue(firebaseAuth.IsSignIn);
        }
    }
}