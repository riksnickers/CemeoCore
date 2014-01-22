using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Dispatcher;
using PushSharpService.Services.Logic;
using log4net;

namespace PushSharpService.Services
{
    [ServiceBehavior( InstanceContextMode = InstanceContextMode.PerSession,
        IncludeExceptionDetailInFaults = true)]
    [InstanceProviderBehavior]
    public class PSService : IPSService
    {
        IDeviceRepository _deviceRepository;
        private readonly ILog Logger = log4net.LogManager.GetLogger(typeof(PSService));
        
        public PSService(IDeviceRepository repository)
        {
            Logger.Debug("Instance of PushSharpService constructed");
            this._deviceRepository = repository;
        }
        public void DoWork()
        {

        }

        public void Send()
        {
            throw new NotImplementedException();
        }

        public void AddUserDevice(Device device)
        {
            this._deviceRepository.AddDevice(device);
        }
    }
}
