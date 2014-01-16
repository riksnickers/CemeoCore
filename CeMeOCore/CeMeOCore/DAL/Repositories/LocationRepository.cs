using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class LocationRepository : GenericRepository<Location>
    {
        public LocationRepository(CeMeoContext dbContext) : base( dbContext)
        {

        }

        
    }
}