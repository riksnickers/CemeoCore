using CeMeOCore.DAL.Models;
using CeMeOCore.Properties;
using log4net;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class Push
    {
        private Push() { }
        private static PushBroker pushBroker;

        public static PushBroker Instance
        {
            get 
            {
                if (pushBroker == null)
                {
                    pushBroker = new PushBroker();

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
            set 
            { 
                pushBroker = value; 
            }
        }        
    }
}