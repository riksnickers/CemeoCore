using CeMeOCore.DAL.Models;
using CeMeOCore.Properties;
using log4net;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class Push
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(Push));
        private Push() { }
        private static PushBroker pushBroker;
        private static ILog log = LogManager.GetLogger(typeof(Push));
        public static PushBroker Instance
        {
            get 
            {
                try
                {
                    if (pushBroker == null)
                    {
                        pushBroker = new PushBroker();

                        pushBroker.OnNotificationSent += NotificationSent;
                        pushBroker.OnChannelException += ChannelException;
                        pushBroker.OnServiceException += ServiceException;
                        pushBroker.OnNotificationFailed += NotificationFailed;
                        pushBroker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
                        pushBroker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
                        pushBroker.OnChannelCreated += ChannelCreated;
                        pushBroker.OnChannelDestroyed += ChannelDestroyed;

                        //Register Apple Service
                        var appleCert = Resources.Pusharp_PuchCert_Development;
                        pushBroker.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, "!CeMeOKeyPass123"));
                        /* pushBroker.RegisterService<AppleNotification>(
                                 new ApplePushService(new ApplePushChannelSettings(false, appleCert, "!CeMeOKeyPass123"))); */

                        //Register Android service
                        pushBroker.RegisterGcmService(new GcmPushChannelSettings("AIzaSyAPRV7yOnM8b2E5rl63X-cbgatBlH1NrFI"));
                    }
                    return pushBroker;
                }
                catch(Exception ex)
                {
                    log.Error(DateTime.Now.ToString() + "\t" + ex.Message + "\t" + ex.Source + "\t" + ex.StackTrace);
                    return null;
                }
            }
            set 
            { 
                pushBroker = value; 
            }
        }

        public static void NotificationSent(object sender, INotification notification)
        {
            Logger.Debug("NotificationSent" + notification.ToString());
        }

        public static void ChannelException(object sender, IPushChannel pushChannel, Exception error)
        {
            Logger.Debug("ChannelException");
        }

        public static void ServiceException(object sender, Exception error)
        {
            Logger.Debug("ServiceException");
        }

        public static void NotificationFailed(object sender, INotification notification, Exception error)
        {
            Logger.Debug("NotificationFailed");
        }

        public static void DeviceSubscriptionExpired(object sender, string expiredSubscriptionId, DateTime expirationDateUtc, INotification notification)
        {
            Logger.Debug("DeviceSubscriptionExpired");
        }

        public static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            Logger.Debug("DeviceSubscriptionChanged");
        }

        public static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Logger.Debug("ChannelCreated");
        }

        public static void ChannelDestroyed(object sender)
        {
            Logger.Debug("ChannelDestroyed");
        }
    }
}