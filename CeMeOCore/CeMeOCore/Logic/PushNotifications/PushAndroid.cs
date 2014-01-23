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
            throw new NotImplementedException();
        }
    }
}