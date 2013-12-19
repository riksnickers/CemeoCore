using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    public class Organiser : IOrganiser
    {
        /// <summary>
        /// An instance of the CeMeOContext
        /// </summary>
        private CeMeoContext _db = new CeMeoContext();

        /// <summary>
        /// The id of the OrganiserID process that will be used.
        /// </summary>
        public string OrganiserID { get; private set; }

        /// <summary>
        /// This is who requested to organise a meeting
        /// </summary>
        private UserProfile RequestedBy { get; set; }
        /// <summary>
        /// This is when the request was send to organise a meeting
        /// </summary>
        private DateTime DateRequested{ get; set;}
        /// <summary>
        /// This is a collection of all Attendees that are invited.
        /// </summary>
        private ICollection<InvitedUser> Attendees { get; set; }
        
        /// <summary>
        /// This is a collection of all the blackspots that are found
        /// </summary>
        private ICollection<BlackSpot> _blackSpots {get; set;}

        /// <summary>
        /// This is the deadline when the meeting must be planned
        /// </summary>
        public int DeadLineInDays { get; set; }

        /// <summary>
        /// To start organising a meeting the constructor must be called
        /// </summary>
        /// <param name="attendees">All of the invited attendees</param>
        /// <param name="dateRequested">When the organiser was started</param>
        /// <param name="deadLineInDays">This is the deadline</param>
        /// <param name="requestedById">Who requested to organise this meeting ID</param>
        public Organiser( IEnumerable<InvitedUser> attendees, DateTime dateRequested, int deadLineInDays, int requestedById )
        {
            //Resolve requestedBy
            RequestedBy = resolveRequestedBy(requestedById);
            //Create unique ID
            //CheckAttendeesCalendar/appointments
            checkAvailabilityAttendees();
            //Do until all attendees can come
            do
            {
                //  CalculateEarliestAppointment - take blackspots in account
                calculateEarliestAppointment();
                //  SendPropositionToAttendees
                sendPropositionToAttendees();
                //  If an attendee denies let it recalucate
            } while (true);

            //If everyone accepted
            //  Plan an appointment in their calendar

            //  CreateMeeting
        }

        /// <summary>
        /// This method will figure out all the blackspots.
        /// </summary>
        private void checkAvailabilityAttendees()
        {
            //Generate blackspots
        }
        
        /// <summary>
        /// This method will calculate the earliest appointment possible.
        /// </summary>
        private void calculateEarliestAppointment()
        {
            
        }

        /// <summary>
        /// This method will send a proposition to all invitees. The Inviter class will handle all these request/respsonses
        /// </summary>
        private void sendPropositionToAttendees()
        {
            Inviter inviter = Startup.InviterManagerFactory().create();
            var res = inviter.sendProposition();
        }

        /// <summary>
        /// This will resolve the id of the creator
        /// </summary>
        /// <param name="userProfileId">The id of who requested the meeting
        /// </param>
        /// <returns></returns>
        private UserProfile resolveRequestedBy( int userProfileId)
        {
            try
            {
                return _db.Users.Find(userProfileId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    
}