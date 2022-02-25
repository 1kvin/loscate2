using System;
using System.Threading.Tasks;
using Loscate.App.Models;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Loscate.DTO.Firebase;
using Loscate.DTO.Social;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.ViewModel.Chat
{
    [TestClass]
    public class DialogsTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";
        
        public DialogsTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            DependencyService.Register<INotificationManager, FakeNotificationManager>();
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
        }
        
         
        [TestMethod]
        public void LoadDialogsTest()
        {
            var viewModel = new DialogsViewModel();

            viewModel.LoadDialogsCommand.Execute(null);
        }
        
        [TestMethod]
        public async Task CheckNewMessageTest()
        {
            var viewModel = new DialogsViewModel();

            await viewModel.CheckNewMessage();
        }
        
        [TestMethod]
        public void ShowNotificationTest()
        {
            var viewModel = new DialogsViewModel();

            viewModel.ShowNotification("test", "msg");
        }
        
        [TestMethod]
        public void SelectDialogTest()
        {
            var viewModel = new DialogsViewModel();

            viewModel.SelectedDialog = new XamarinDialog(new Dialog()
            {
                Companion = new FirebaseUser(testPinUserUID, "name", "email", "pic", "photo")
            });
        }
    }
}