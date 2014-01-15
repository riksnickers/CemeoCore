using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting>
    {
        public MeetingRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }
    }
}