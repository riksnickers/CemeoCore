using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushSharpService.Services.Logic
{
    public class DeviceRepository : IDeviceRepository
    {
        public HashSet<Device> _devices;
        private readonly ILog Logger = log4net.LogManager.GetLogger(typeof(DeviceRepository));

        public DeviceRepository()
        {
            this._devices = new HashSet<Device>();
        }

        public IEnumerable<Device> GetUserDevice(int userID)
        {
            Logger.Debug("Getting a device from the list");
            IEnumerable<Device> found = from device in this._devices
                                        where device.userID == userID
                                        select device;
            return found;
        }

        public void AddDevice( Device d )
        {
            Logger.Debug("Adding a device to the list");
            try
            {
                this._devices.Add(d);
            }
            catch( Exception )
            {

            }
        }
    }
}