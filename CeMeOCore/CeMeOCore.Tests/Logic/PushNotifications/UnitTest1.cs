using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CeMeOCore.DAL.Models;
using CeMeOCore.Logic.PushNotifications;

namespace CeMeOCore.Tests.Logic.PushNotifications
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ApplePushTest()
        {
            Device d = new Device();
            d.DeviceID = "fc0e50bb4200be9a719e1650c01171d72ec8d9d29c39acc0e14ae8638bc5e4c4";
            d.Platform = Platform.Apple;
            d.userID = 1;

            PushContext pc = new PushContext();
            pc.Send(d, "Hoi jef!");
        }
    }
}
