using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class PropositionRepository : GenericRepository<Proposition>
    {
        /// <summary>
        /// This constructor will pass the context through the base
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public PropositionRepository( CeMeoContext dbContext ) : base(dbContext)
        {

        }
    }
}