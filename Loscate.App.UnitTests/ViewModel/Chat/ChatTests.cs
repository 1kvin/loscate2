using System;
using Loscate.App.Map;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.Forms;

namespace Loscate.App.UnitTests.ViewModel.Chat
{
    [TestClass]
    public class ChatTests
    {
        private const string testPinUserUID = "XpAJR94ioncT4C6mJg5ilz7hKSE2";

        public ChatTests()
        {
            DependencyService.Register<IFirebaseAuthenticator, FakeAuthenticator>();
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
        }
        
        [TestMethod]
        public void LoadTest()
        {
            var viewModel = new ChatPageViewModel();
            viewModel.LoadMessageCommand.Execute(null);
        }
        
        [TestMethod]
        public void OnSendTest()
        {
            var viewModel = new ChatPageViewModel();
            viewModel.OnSendCommand.Execute(null);
        }
        
        [TestMethod]
        public void OnMessageAppearingTest()
        {
            var viewModel = new ChatPageViewModel();
            viewModel.MessageAppearingCommand.Execute(null);
        }
        
        [TestMethod]
        public void OnMessageDisappearingTest()
        {
            var viewModel = new ChatPageViewModel();
            viewModel.MessageDisappearingCommand.Execute(null);
        }
        
        [TestMethod]
        public void OnSendMessageTest()
        {
            var viewModel = new ChatPageViewModel
            {
                CompanionName = "testUser",
                CompanionUserUID = testPinUserUID,
                TextToSend = Guid.NewGuid().ToString()
            };
            viewModel.MessageDisappearingCommand.Execute(null);
        }
    }
}