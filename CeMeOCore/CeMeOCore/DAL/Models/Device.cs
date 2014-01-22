using CeMeOCore.Logic.PushNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class Device
    {
        public string DeviceID { get; set; }
        public Platform Platform { get; set; }
        public int userID { get; set; }
    }
}