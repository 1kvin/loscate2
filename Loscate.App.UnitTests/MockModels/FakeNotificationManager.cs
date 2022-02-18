using System;
using System.Collections.Generic;
using Loscate.App.Services;

namespace Loscate.App.UnitTests
{
    public class FakeNotificationManager : INotificationManager
    {
        public event EventHandler NotificationReceived;

        public List<(string title, string message, DateTime? notifyTime)> SendNotifications;
        
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void SendNotification(string title, string message, DateTime? notifyTime = null)
        {
            SendNotifications.Add((title, message, notifyTime));
        }

        public void ReceiveNotification(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}