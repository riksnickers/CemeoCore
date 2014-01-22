using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeOCore.Logic.PushNotifications
{
    interface IPushPlatform
    {
        void Send(Device device, string message);
    }
}
