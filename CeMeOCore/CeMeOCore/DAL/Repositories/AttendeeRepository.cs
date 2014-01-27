using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    public class AttendeeRepository : GenericRepository<Attendee>
    {
        public AttendeeRepository(CeMeoContext dbContext): base(dbContext)
        {

        }

        public IEnumerable<Attendee> GetAttendings(int userid)
        {
            return this.dbSet.Where(a => a.UserId == userid).Select(a => a);
        }

        public IEnumerable<int> GetAttendeesIdByMeetingId(int meetingID)
        {
            return this.dbSet.Where(a => a.MeetingId == meetingID).Select(a => a.UserId);
        }

    }
}