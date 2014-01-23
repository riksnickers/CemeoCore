using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel;
using PushSharpService.Services.Logic;
using log4net;

namespace PushSharpService.Services
{
    public class InstanceProviderBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly ILog Logger = log4net.LogManager.GetLogger(typeof(InstanceProviderBehaviorAttribute));
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher ed in cd.Endpoints)
                {
                    if (!ed.IsSystemEndpoint)
                    {
                        ed.DispatchRuntime.InstanceProvider = new ServiceInstanceProvider();
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        class ServiceInstanceProvider : IInstanceProvider
        {
            public object GetInstance(InstanceContext instanceContext, Message message)
            {
                IDeviceRepository repository = DeviceRepositoryFactory.Instance.CreateDeviceRepository();
                return new PSService(repository);
            }

            public object GetInstance(InstanceContext instanceContext)
            {
                return this.GetInstance(instanceContext, null);
            }

            public void ReleaseInstance(InstanceContext instanceContext, object instance)
            {
            }
        }
    }
}