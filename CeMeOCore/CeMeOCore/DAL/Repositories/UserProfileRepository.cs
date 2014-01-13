using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>
    {
        public UserProfileRepository( CeMeoContext dbContext ) : base( dbContext )
        {

        }

        public IEnumerable<Object> GetContactsCompact()
        {
            var users = context.Users.Select(u => new { id = u.UserId, FirstName = u.FirstName, LastName = u.LastName }).ToList();
            return users;
        }
    }
}