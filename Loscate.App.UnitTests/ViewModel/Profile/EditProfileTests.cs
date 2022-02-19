using System;
using System.IO;
using System.Threading.Tasks;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.ViewModel.Profile
{
    [TestClass]
    public class EditProfileTests
    {
        public EditProfileTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
        }
        
        [TestMethod]
        public void SaveChangesTest()
        {
            var viewModel = new EditProfileViewModel
            {
                base64photo = Guid.NewGuid().ToString()
            };
            
            viewModel.SaveChangesCommand.Execute(null);
        }
        
        [TestMethod]
        public void UserNameMinLimitTest()
        {
            var viewModel = new EditProfileViewModel
            {
                UserName = "123"
            };

            Assert.AreEqual(viewModel.ValidateUserName(), "Имя пользователя, не может быть меньше 10 символов.");
        }
        
        [TestMethod]
        public void UserNameMaxLimitTest()
        {
            var viewModel = new EditProfileViewModel
            {
                UserName = "123456789012345678901234567890123456789012345678901234567890"
            };

            Assert.AreEqual(viewModel.ValidateUserName(), "Имя пользователя, не может быть больше 30 символов.");
        }
    }
}