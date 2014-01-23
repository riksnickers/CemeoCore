using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetUserDevice(int userID);

        void AddDevice( Device d );
    }
}