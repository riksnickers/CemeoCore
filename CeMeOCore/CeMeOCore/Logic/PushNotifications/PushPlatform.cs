using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public abstract class PushPlatform : IPushPlatform
    {
        public abstract void Send(Device device, string message);
    }
}