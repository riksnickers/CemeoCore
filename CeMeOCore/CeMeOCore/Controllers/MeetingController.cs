using CeMeOCore.Logic.MeetingOrganiser;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CeMeOCore.Controllers
{
    ///<summary>
    ///This is a API controller to maintain meetings
    ///</summary>
    //[Authorize]
    [RoutePrefix("api/Meeting")]
    public class MeetingController : ApiController
    {
        private CeMeoContext _db = new CeMeoContext();
        ///<summary>
        ///  Get a specific meeting
        ///  This is a GET method
        ///</summary>
        ///<param name="id"></param>
        [AcceptVerbs("GET")]
        public IEnumerable<String> Get(int id)
        {
            return new string[] { id.ToString() };
        }

        ///<summary>
        ///  Update a specific meeting request (for now its only the deadline)
        ///  This is a PUT method
        ///</summary>
        ///<param name="model"></param>
        [AcceptVerbs("PUT")]
        [Route("Deadline")]
        public Boolean Put([FromBody]ChangeDeadlineMeetingBindingModel model)
        {
            return false;
        }
        
        ///<summary>
        ///  Returns last x meetings
        ///  This is a GET method
        ///</summary>
        ///<param name="count">(Optional)If you would like to have the 10 last meetings you enter 10 for count.</param>
        ///<param name="from">(Optional)If you would like to have the 5 last meetings starting from the 12meeting you enter 5 for count and 12 for from.</param>
        [AcceptVerbs("GET")]
        [Route("last")]
        public IEnumerable<string> GetLast(int count = 1, int from = 0)
        {
            return new string[] { "Meeting from: " + count + " beginning from: " + from };
        }

        ///<summary>
        ///  Return x latest upcomming meetings
        ///  This is a GET method
        ///</summary>
        ///<param name="latest">This is how many meetings you want</param>
        [AcceptVerbs("GET")]
        [Route("Upcomming")]
        public IEnumerable<String> GetUpcomming(int latest = 1)
        {
            return new string[]{"Latest "+ latest +" Upcomming "};
        }


        // POST /api/Meeting/schedule
        // To schedule a meeting
        /// <summary>
        ///   This will start scheduling a meeting.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("Schedule")]
        [ResponseType(typeof(ScheduleMeetingBindingModel))]
        public IHttpActionResult Schedule([FromBody]ScheduleMeetingBindingModel model)
        {
            return Ok(model);
        }

        
        // 
        // 
        // POST /api/Meeting/Cancel
        /// <summary>
        ///   No delete function because the system will delete/archive it.
        ///   We are using a cancel function
        ///   This is a POST method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("Cancel")]
        public Boolean Cancel([FromBody]CancelMeetingBindingModel model)
        {
            return false;
        }

        /// <summary>
        /// When the server sends a pushnotification/payload
        /// it includes an ID that identifies Which inviter is being used.
        /// </summary>
        /// <param name="model"><seealso cref="InviterAnswerBindingModel"/></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("InviteResponse")]
        public IHttpActionResult InviteResponse([FromBody]InviterAnswerBindingModel model)
        {
            switch(model.Answer)
            {
                case Availability.Absent: break;
                case Availability.Present: break;
                case Availability.Online: break;
                default: break;
            }

            Startup.InviterManagerFactory().getInviter(model.InviterID).registerAvailabilityAttendee(model.InviteeID, model.Answer);
            return Ok();
        }

        /// <summary>
        /// This method will return contacts.
        /// This is a GET method
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("Contacts")]
        public IEnumerable<Object> GetContacts()
        {
            var users = _db.Users.Select(u => new { id = u.UserId, FirstName = u.FirstName, LastName = u.LastName }).ToList();
            return users;
        }
    }
}
