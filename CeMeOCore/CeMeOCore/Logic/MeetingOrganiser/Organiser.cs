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
        private ICollection<InvitedUser> Attendees { get; set; }
        
        private ICollection<BlackSpot> _blackSpots {get; set;}
        public int DeadLineInDays { get; set; }
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

        private void checkAvailabilityAttendees()
        {
            //Generate blackspots
        }
        
        private void calculateEarliestAppointment()
        {
            
        }

        private void sendPropositionToAttendees()
        {
            Inviter inviter = Startup.InviterManagerFactory().create();
            var res = inviter.sendProposition();
        }

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

    public class OrganiserManager
    {
        Dictionary<string, Organiser> dictionary;
        public OrganiserManager()
        {
            dictionary = new Dictionary<string,Organiser>();
        }

        private Organiser getOrganiser( string organiserId )
        {
            if (dictionary.ContainsKey(organiserId))
	        {
                return dictionary[organiserId];
	        }
            else return null;
        }

        public Boolean addOrganiser( Organiser organiser )
        {
            try 
	        {	        
		        dictionary.Add(organiser.OrganiserID, organiser);
                return true;
	        }
	        catch (Exception)
	        {
		        return false;
	        }
        }
    }
}