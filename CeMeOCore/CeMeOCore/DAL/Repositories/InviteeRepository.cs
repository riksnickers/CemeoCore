using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    /// <summary>
    /// This is the InviteeRepository that extends the GenericRepository
    /// </summary>
    public class InviteeRepository : GenericRepository<Invitee>
    {
        /// <summary>
        /// This constructor will pass the context through the base
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public InviteeRepository(CeMeoContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Get all invitees by profile user id.
        /// Because a user can be invited to multiple meetings, a new invitee will be created for this user.
        /// </summary>
        /// <param name="upID">UserProfileID</param>
        /// <returns>IEnumerable<Invitee></returns>
        public IEnumerable<Invitee> GetInviteeIDsByUserProfileID(int upID)
        {
            return this.dbSet.Where(u => u.UserID == upID).Select(u => u).ToList();
        }
    }
}