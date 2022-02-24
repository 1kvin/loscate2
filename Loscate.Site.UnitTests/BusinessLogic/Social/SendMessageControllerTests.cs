using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loscate.Site.Controllers.Social.Message;
using System;
using System.Collections.Generic;
using Loscate.Site.DbContext;
using Moq;
using Microsoft.EntityFrameworkCore;
using Loscate.Site.Services;
using System.Security.Claims;
using Loscate.Site.Repository;
using System.Linq;

namespace Loscate.Site.Controllers.Social.Message.Tests
{
    [TestClass()]
    public class SendMessageControllerTests
    {
        const int dialogId = 0;
        const int testUserId1 = 1;
        const string testUserUid1 = "uid";
        const int testUserId2 = 2;
        const string testUserUid2 = "uidi";
        const string testMessageText = "fgfgdg";

        [TestMethod()]
        public void GetLimitTest()
        {

            var testUser1 = new FirebaseUser { Id = testUserId1, Uid = testUserUid1 };
            var testUser2 = new FirebaseUser { Id = testUserId2, Uid = testUserUid2 };

            var users = new List<FirebaseUser>()
            {
                testUser1,
                testUser2
            }.AsQueryable();

            var message = new ChatMessage()
            {
                Text = testMessageText,
                Time = DateTime.UtcNow,
                DialogId = dialogId,
                SendUser = testUser1,
                SendUserId = testUser1.Id
            };
            var chatMessages = new List<ChatMessage>()
            {
                message
            }.AsQueryable();

            var dialogs = new List<DbContext.Dialog>()
            {
                new DbContext.Dialog()
                {
                    Id = dialogId,
                    UserId1 = testUserId1,
                    UserId2 = testUserId2,
                    UserId1Navigation = testUser1,
                    UserId2Navigation = testUser2
                }

            }.AsQueryable();


            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var chatMockSet = new Mock<DbSet<ChatMessage>>();
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Provider).Returns(chatMessages.Provider);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Expression).Returns(chatMessages.Expression);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.ElementType).Returns(chatMessages.ElementType);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.GetEnumerator()).Returns(chatMessages.GetEnumerator());

            var dialogMockSet = new Mock<DbSet<DbContext.Dialog>>();
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Provider).Returns(dialogs.Provider);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Expression).Returns(dialogs.Expression);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.ElementType).Returns(dialogs.ElementType);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.GetEnumerator()).Returns(dialogs.GetEnumerator());

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser1);

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.ChatMessages).Returns(chatMockSet.Object);
            mockContextDb.Setup(c => c.Dialogs).Returns(dialogMockSet.Object);
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var controller = new SendMessageController(mockContextDb.Object, mockUserService.Object);
            var msq = new string('a',251);
            var result = controller.Get(testUser2.Uid, msq);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "LIMIT");
        }

        [TestMethod()]
        public void GetNullToUserTest()
        {

            var testUser1 = new FirebaseUser { Id = testUserId1, Uid = testUserUid1 };
            var testUser2 = new FirebaseUser { Id = testUserId2, Uid = testUserUid2 };

            var users = new List<FirebaseUser>()
            {
                testUser1
            }.AsQueryable();

            var message = new ChatMessage()
            {
                Text = testMessageText,
                Time = DateTime.UtcNow,
                DialogId = dialogId,
                SendUser = testUser1,
                SendUserId = testUser1.Id
            };
            var chatMessages = new List<ChatMessage>()
            {
                message
            }.AsQueryable();

            var dialogs = new List<DbContext.Dialog>()
            {
                new DbContext.Dialog()
                {
                    Id = dialogId,
                    UserId1 = testUserId1,
                    UserId2 = testUserId2,
                    UserId1Navigation = testUser1,
                    UserId2Navigation = testUser2
                }

            }.AsQueryable();


            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var chatMockSet = new Mock<DbSet<ChatMessage>>();
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Provider).Returns(chatMessages.Provider);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Expression).Returns(chatMessages.Expression);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.ElementType).Returns(chatMessages.ElementType);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.GetEnumerator()).Returns(chatMessages.GetEnumerator());

            var dialogMockSet = new Mock<DbSet<DbContext.Dialog>>();
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Provider).Returns(dialogs.Provider);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Expression).Returns(dialogs.Expression);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.ElementType).Returns(dialogs.ElementType);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.GetEnumerator()).Returns(dialogs.GetEnumerator());

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser1);

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.ChatMessages).Returns(chatMockSet.Object);
            mockContextDb.Setup(c => c.Dialogs).Returns(dialogMockSet.Object);
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var controller = new SendMessageController(mockContextDb.Object, mockUserService.Object);
            var msq = new string('a', 249);
            var result = controller.Get(testUser2.Uid, msq);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "nullToUser");
        }

        [TestMethod()]
        public void GetOkTest()
        {

            var testUser1 = new FirebaseUser { Id = testUserId1, Uid = testUserUid1 };
            var testUser2 = new FirebaseUser { Id = testUserId2, Uid = testUserUid2 };

            var users = new List<FirebaseUser>()
            {
                testUser1,
                testUser2
            }.AsQueryable();

            var message = new ChatMessage()
            {
                Text = testMessageText,
                Time = DateTime.UtcNow,
                DialogId = dialogId,
                SendUser = testUser1,
                SendUserId = testUser1.Id
            };
            var chatMessages = new List<ChatMessage>()
            {
                message
            }.AsQueryable();

            var dialogs = new List<DbContext.Dialog>()
            {
                new DbContext.Dialog()
                {
                    Id = dialogId,
                    UserId1 = testUserId1,
                    UserId2 = testUserId2,
                    UserId1Navigation = testUser1,
                    UserId2Navigation = testUser2
                }

            }.AsQueryable();


            var userMockSet = new Mock<DbSet<FirebaseUser>>();
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Provider).Returns(users.Provider);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.Expression).Returns(users.Expression);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userMockSet.As<IQueryable<FirebaseUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var chatMockSet = new Mock<DbSet<ChatMessage>>();
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Provider).Returns(chatMessages.Provider);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.Expression).Returns(chatMessages.Expression);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.ElementType).Returns(chatMessages.ElementType);
            chatMockSet.As<IQueryable<ChatMessage>>().Setup(m => m.GetEnumerator()).Returns(chatMessages.GetEnumerator());

            var dialogMockSet = new Mock<DbSet<DbContext.Dialog>>();
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Provider).Returns(dialogs.Provider);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.Expression).Returns(dialogs.Expression);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.ElementType).Returns(dialogs.ElementType);
            dialogMockSet.As<IQueryable<DbContext.Dialog>>().Setup(m => m.GetEnumerator()).Returns(dialogs.GetEnumerator());

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(c => c.GetDbUser(It.IsAny<ClaimsPrincipal>())).Returns(testUser1);

            var mockContextDb = new Mock<ILoscateDbRepository>();
            mockContextDb.Setup(c => c.ChatMessages).Returns(chatMockSet.Object);
            mockContextDb.Setup(c => c.Dialogs).Returns(dialogMockSet.Object);
            mockContextDb.Setup(c => c.FirebaseUsers).Returns(userMockSet.Object);

            var controller = new SendMessageController(mockContextDb.Object, mockUserService.Object);
            var msq = new string('a', 249);
            var result = controller.Get(testUser2.Uid, msq);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "OK");
        }
    }
}