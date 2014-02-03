using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.Logic.Organiser;
using CeMeOCore.DAL.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CeMeOCore.Controllers
{
    ///<summary>
    ///This is a API controller to maintain meetings
    ///</summary>
    [Authorize]
    [RoutePrefix("api/Meeting")]
    public class MeetingController : ApiController
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(MeetingController));
        private MeetingControllerUoW _meetingUoW;

        public MeetingController ()
        {
            this._meetingUoW = new MeetingControllerUoW();
        }

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

        /// <summary>
        /// Get all meetings from logged in user.
        /// </summary>
        /// <returns></returns>
        [Route("All")]
        public IEnumerable<MeetingInformation> Get()
        {
            HashSet<MeetingInformation> mih = new HashSet<MeetingInformation>();
            string id = User.Identity.GetUserId();
            int idUP = this._meetingUoW.UserProfileRepository.Get(u => u.aspUser == id).Select(u => u.UserId).First();
           
            List<Attendee> attendings = this._meetingUoW.AttendeeRepository.GetAttendings(idUP).ToList();
            foreach( Attendee attendee in attendings )
            {
                MeetingInformation mi = new MeetingInformation();
                mi.Self = attendee;
                /*List<int> OtherId = this._meetingUoW.AttendeeRepository.GetAttendeesIdByMeetingId(attendee.MeetingId).ToList();
                foreach( int userId in OtherId )
                {
                    mi.Others.Add(this._meetingUoW.UserProfileRepository.GetByIDCompact(userId));
                }*/
                foreach (Meeting meeting in attendee.Meetings)
                {
                    mi.Meeting = meeting;
                    foreach (Attendee other in meeting.Attendees)
                    {
                        mi.Others.Add(this._meetingUoW.UserProfileRepository.GetByIDCompact(other.UserId));
                    }
                }
                mih.Add(mi);
            }
           

             return mih;
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
        [Route("Upcoming")]
        public IEnumerable<String> GetUpcomming(int latest = 1)
        {
            log.Debug("GetUpcoming");
            return new string[]{"Latest "+ latest +" Upcoming "};
        }

        /// <summary>
        ///   This will start scheduling a meeting.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("Schedule")]
        public IHttpActionResult Schedule(HttpRequestMessage mes, [FromBody]ScheduleMeetingBindingModel model)
        {
            log.Debug(DateTime.Now.ToString() + "\t" + "Ok, create meeting..");
            OrganiserManager.Create(model);
            log.Debug(DateTime.Now.ToString() + "\t" + "Ok, meeting created..");
            return Ok();
        }

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
        /// Not used, can be maybe deleted.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("Proposition")]
        public Proposition GetPropositions(int userid)
        {
            return null;
        }



        
        /// <summary>
        /// Dispose the context and the controller
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this._meetingUoW.Dispose();
            base.Dispose(disposing);
        }
    }
}
