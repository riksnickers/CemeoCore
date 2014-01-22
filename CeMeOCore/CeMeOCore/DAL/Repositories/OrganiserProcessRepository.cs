using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class OrganiserProcessRepository : GenericRepository<OrganiserProcess>
    {
        public OrganiserProcessRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<string> GetOrganiserIdWhenNotFinished()
        {
            return dbSet.Where(op => op.Status != OrganiserStatus.FinishedOrganising).Select(op => op.OrganiserID);
        }
    }
}