using CeMeOCore.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class InviteeRepository : GenericRepository<Invitee>
    {
        public InviteeRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Invitee> GetInviteeIDsByUserProfileID(int upID)
        {
            return this.dbSet.Where(u => u.UserID == upID);
        }
    }
}