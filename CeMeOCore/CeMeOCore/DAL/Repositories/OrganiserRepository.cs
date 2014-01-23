using CeMeOCore.DAL.Context;
using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class OrganiserRepository : GenericRepository<Organiser>
    {
        /// <summary>
        /// Soon
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public OrganiserRepository( CeMeoContext dbContext ) : base( dbContext )
        {

        }
    }
}