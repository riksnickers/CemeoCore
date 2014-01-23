using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushSharpService.Services.Logic
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetUserDevice(int userID);

        void AddDevice( Device d );
    }
}