using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class DeviceManager
    {
        private static HashSet<Device> devices = new HashSet<Device>();

        public static void CreateDevice( string deviceID, Platform platform, int userId)
        {
            Device device = devices.Where(d => d.userID == userId && d.Platform == platform).Select(d => d).FirstOrDefault();
            if (device == null)
            {
                devices.Add(new Device() { DeviceID = deviceID, Platform = platform, userID = userId });
            }
            else
            {
                device.DeviceID = deviceID;
            }
        }

        public static IEnumerable<Device> GetDevicesFromUser( int userId )
        {
            return from device in devices
                   where device.userID == userId
                   select device;
        }
    }
}