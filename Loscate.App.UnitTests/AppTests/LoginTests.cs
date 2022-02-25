using Loscate.App.Services;
using Loscate.App.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.AppTests
{
    [TestClass]
    public class LoginTests
    {
        private readonly FakeAuthenticator fakeAuthenticator;
        public LoginTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            Xamarin.Forms.Mocks.MockForms.Init();
            
            fakeAuthenticator = (FakeAuthenticator)DependencyService.Resolve<IFirebaseAuthenticator>();
        }

        [TestMethod]
        public void AlreadyLoginTest()
        {
            fakeAuthenticator.HaveUser = false;
            Application.Current = new App();

            Assert.AreEqual(Application.Current.MainPage.GetType(), typeof(AppShell));
        }
        
        [TestMethod]
        public void HaveUserDataTest()
        {
            Application.Current = new App();

            Assert.AreEqual(Application.Current.MainPage.GetType(), typeof(AppShell));
        }
        
        [TestMethod]
        public void TokenDeprecatedTest()
        {
            fakeAuthenticator.TestUserData = string.Empty;
            Application.Current = new App();

            Assert.AreEqual(Application.Current.MainPage.GetType(), typeof(LoginPage));
        }
    }
}