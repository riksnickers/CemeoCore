using CeMeOCore.DAL.Models;
using CeMeOCore.Properties;
using log4net;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class PushApple : PushPlatform
    {
        private ILog log = LogManager.GetLogger(typeof(PushApple));
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
                log.Error(DateTime.Now.ToString() + "\t" + ex.Message + "\t" + ex.Source + "\t" + ex.StackTrace);
                throw;
            }
            
        }
    }
}