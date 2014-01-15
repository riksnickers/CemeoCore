using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class PropositionRepository : GenericRepository<Proposition>
    {
        public PropositionRepository( CeMeoContext dbContext ) : base(dbContext)
        {

        }
    }
}