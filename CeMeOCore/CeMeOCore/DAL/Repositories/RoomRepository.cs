using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        /// <summary>
        /// This constructor will pass the context through the base
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public RoomRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }
    }
}