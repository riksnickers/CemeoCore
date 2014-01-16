using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PushSharp;
using System.IO;
using PushSharp.Apple;

namespace CeMeOCore.Logic.PushNotifications
{
    public class ApplePushNotification
    {
        public ApplePushNotification()
        {
            //Push broker nog omzetten naar singleton
            var push = new PushBroker();
            var appleCert = Properties.Resources.CeMeOPush;
            push.RegisterAppleService(new ApplePushChannelSettings(appleCert, "!CeMeOKeyPass123"));
            //push.QueueNotification(new AppleNotification().ForDeviceToken().WithAlert().WithBadge().WithSound());
        }
    }
}