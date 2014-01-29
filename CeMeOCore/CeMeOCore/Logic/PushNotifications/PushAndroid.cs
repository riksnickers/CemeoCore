using PushSharp.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class PushAndroid : PushPlatform
    {
        public override void Send(DAL.Models.Device device, string message)
        {
            GcmNotification notification = new GcmNotification();
            notification.RegistrationIds.Add(device.DeviceID);
            notification.JsonData = "{\"alert\":\""+message+"\",\"badge\":1,\"sound\":\"sound.caf\"}";
            Push.Instance.QueueNotification(notification);
        }
    }
}