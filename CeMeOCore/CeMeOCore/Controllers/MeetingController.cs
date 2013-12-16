using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Meeting")]
    public class MeetingController : ApiController
    {
        // GET /api/Meeting/<meetingID>
        // Get a specific meeting
        [AcceptVerbs("GET")]
        public IEnumerable<String> Get(int id)
        {
            return new string[] { id.ToString() };
        }

        // PUT /api/Meeting/
        // Update a specific meeting
        [AcceptVerbs("PUT")]
        [Route("Deadline")]
        public Boolean Put([FromBody]ChangeDeadlineMeetingBindingModel model)
        {
            return false;
        }

        // GET /api/Meeting/count=10&from=10&
        // Returns last x meetings
        [AcceptVerbs("GET")]
        [Route("last")]
        public IEnumerable<string> GetLast(int count = 1, int from = 0)
        {
            return new string[] { "Meeting from: " + count + " beginning from: " + from };
        }

        // GET /api/Meeting/Upcomming?last=10
        // Return x latest upcomming meetings
        [AcceptVerbs("GET")]
        [Route("Upcomming")]
        public IEnumerable<String> GetUpcomming(int latest = 1)
        {
            return new string[]{"Latest "+ latest +" Upcomming "};
        }


        // POST /api/Meeting/schedule
        // To schedule a meeting
        [AcceptVerbs("POST")]
        [Route("Schedule")]
        public Boolean Schedule([FromBody]ScheduleMeetingBindingModel model)
        {
            return false;
        }

        
        // No delete function because the system will delete/archive it.
        // We are using a cancel function
        // POST /api/Meeting/Cancel
        [AcceptVerbs("POST")]
        [Route("Cancel")]
        public Boolean Cancel([FromBody]CancelMeetingBindingModel model)
        {
            return false;
        }
    }
}
