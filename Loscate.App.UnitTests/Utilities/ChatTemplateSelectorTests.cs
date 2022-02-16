using System.Linq;
using System.Reflection;
using Loscate.App.Models;
using Loscate.App.Repository;
using Loscate.App.Utilities;
using Loscate.DTO.Firebase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.TinyIoc;
using Xamarin.Forms.Platform.WPF.Extensions;

namespace Loscate.App.UnitTests.Utilities
{
    [TestClass]
    public class ChatTemplateSelectorTests
    {
        private const string testMessageText = "testText";
        private const string user1name = "user1";
        private const string user2name = "user2";

        private const string photoBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAACAAAAAeCAYAAABNChwpAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAGSSURBVFhH7Zc9SgNBGIZXET2AlRFSeQFBWxPSCYK3SMqgJ7DxDnaewVqQ2AfvYKGdlZWVvk92dpBk2O/bdcKi+MDL/GTmzfxlvknxT9dshNTLjjSSzqUjqSftSm/SqzSX7qQH6UPKxpY0ll6kR+lSYgB70nZIKVPP57SjPf1+TF96ku6lQyoc0I729KN/a44lZjNdlJpDP/rj0xhGTuezRak99Men0Uqwdyxf25kvgw9+7jMxkdhDi2vpPaQW+HEwTfipsWSeA8eXf4bUAj988a/lVOKn5KHJCgC++NdyI12U2exwT+BfC7cZl4rFQLqV2IIrKhzgi39kM6Tf4XplryxYzucy6wZf/COpAVR3+zrAF/9IagArjTKyMrnUAIhq+2U2O/jiH0kNgENyUmazg695CInnxPt1gC/+kdQAeEwcSN7Q6wU/fPE34c62YsFQmkncA6SU63DHAug8GkKn74GKTl9EFYyc5WMPvQeTdrSnX6uZL8Pe8UhhNsQAomXqVUx99Sqmvbnnv+Z/wV+lKL4ADb1YGuK+EbUAAAAASUVORK5CYII=";
        
        
        [TestMethod]
        public void OutgoingChatTemplateSelect()
        {
            var msg = new Message
            {
                Text = testMessageText,
                User = user2name
            };

            Assert.AreEqual(GetViewCell(msg), "Loscate.App.Views.Cells.OutgoingViewCell"); 
        }
        
        [TestMethod]
        public void IncomingChatTemplateSelect()
        {
            var msg = new Message
            {
                Text = testMessageText,
                User = user1name
            };

            Assert.AreEqual(GetViewCell(msg), "Loscate.App.Views.Cells.IncomingViewCell"); 
        }
        
        private string GetViewCell(Message msg)
        {
            var userRepository = new UserRepository();
            var user = new FirebaseUser
            {
                Name = user1name,
                PhotoBase64 = photoBase64
            };
            userRepository.user = new XamarinUser(user);

            TinyIoCContainer.Current.Register(userRepository);

            var selector = new ChatTemplateSelector();

            var template = selector.SelectTemplate(msg, null);

            return template.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Single(t => t.Name == "_idString")
                .GetValue(template).ToString();
        }
    }
}