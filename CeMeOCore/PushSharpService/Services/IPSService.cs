using PushSharpService.Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PushSharpService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPushSharp" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IPSService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        void Send();
        [OperationContract(IsInitiating = true)]
        void AddUserDevice(Device device);
    }
}
