using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }
    }
}