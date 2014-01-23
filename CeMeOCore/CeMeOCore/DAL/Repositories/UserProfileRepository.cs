using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>
    {
        /// <summary>
        /// This constructor will pass the context through the base
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public UserProfileRepository( CeMeoContext dbContext ) : base( dbContext )
        {

        }
        
        /// <summary>
        /// Get a compact verion of the contacts
        /// </summary>
        /// <returns>IEnumerable<Object></returns>
        public IEnumerable<Object> GetContactsCompact()
        {
            var users = context.Users.Select(u => new { id = u.UserId, FirstName = u.FirstName, LastName = u.LastName }).ToList();
            return users;
        }

        public UserProfileCompact GetByIDCompact( int id )
        {
            return this.dbSet.Where(u => u.UserId == id).Select(u => new UserProfileCompact { FirstName = u.FirstName, LastName = u.LastName, UserId = u.UserId }).FirstOrDefault();
        }
    }
}