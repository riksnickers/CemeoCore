using CeMeOCore.DAL.Models;
using CeMeOCore.Properties;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class PushApple : PushPlatform
    {
        public PushApple()
        {

        }
        public override void Send(Device device, string message)
        {
            try
            {
                var applenot = new AppleNotification(device.DeviceID, new AppleNotificationPayload(message, 1, "sound.caf"));
                Push.Instance.QueueNotification(applenot);
            }
            catch (NotificationFailureException ex)
            {
                Console.WriteLine(ex.ErrorStatusDescription);
                throw;
            }
            
        }
    }
}