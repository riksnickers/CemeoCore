using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushSharpService.Services.Logic
{
    public class DeviceRepositoryFactory
    {
        private static DeviceRepositoryFactory instance;
        private IDeviceRepository repository;
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(DeviceRepository));

        private DeviceRepositoryFactory() { }

        public static DeviceRepositoryFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    Logger.Debug("Instance == null so construct a new one");
                    instance = new DeviceRepositoryFactory();
                }

                return instance;
            }
        }

        public void SetDeviceRepository(IDeviceRepository repository)
        {
            Logger.Debug("SetDeviceRepository");
            this.repository = repository;
        }

        public IDeviceRepository CreateDeviceRepository()
        {
            Logger.Debug("CreateDeviceRepository");
            return this.repository;
        }
    }
}