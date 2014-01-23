using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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


        public IEnumerable<Invitee> GetInviteeByOrganiserID( string organiserID )
        {
            return this.dbSet.Where(u => u.OrganiserID == organiserID).Select(u => u).ToList();
        }

        /// <summary>
        /// Update the specific entity (will check for changed fields, not whole objects)
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public override void Update(Invitee entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}