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
        //TODO limits
    }
}