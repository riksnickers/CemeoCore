using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser
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
        /// All Participants for this process
        /// </summary>
        private Dictionary<string, Invitee> _invitees { get; set; }
        
        /// <summary>
        /// This is the deadline when the meeting must be planned
        /// </summary>
        public int DeadLineInDays { get; set; }


        private int TotalInvitees { get; set; }
        private int TotalInviteesAbsent { get; set; }
        private int TotalImporantInviteesAbsent { get; set; }
        private int TotalInviteesOnline { get; set; }
        private int TotalInviteesUnanswered { get; set; }
        
        /// <summary>
        /// To start organising a meeting the constructor must be called
        /// </summary>
        /// <param name="participants">All of the invited attendees</param>
        /// <param name="dateRequested">When the organiser was started</param>
        /// <param name="deadLineInDays">This is the deadline</param>
        /// <param name="requestedById">Who requested to organise this meeting ID</param>
        public Organiser(IEnumerable<InvitedParticipant> participants, DateTime dateRequested, DateIndex deadLineInDays, string requestedById)
        {
            //Set counters to 0
            TotalImporantInviteesAbsent = 0;
            TotalInvitees = 0;
            TotalInviteesAbsent = 0;
            TotalInviteesOnline = 0;
            TotalInviteesUnanswered = 0;

            //Resolve requestedBy
            RequestedBy = resolveRequestedBy(requestedById);
            //Create unique ID
            OrganiserID = "xxx";

            //Resolve invitedParticipants
            ConvertParticipantsToInvitees(participants);
           
            //CheckAttendeesCalendar/appointments
            checkAvailabilityAttendees();

            //  CalculateEarliestAppointment - take blackspots in account
            calculateEarliestAppointment();

            sendPropositionToInvitees();
            //  If an attendee denies let it recalucate

            //If everyone accepted
            //  Plan an appointment in their calendar
            //  CreateMeeting
        }


        public Boolean ConvertParticipantsToInvitees(IEnumerable<InvitedParticipant> invitedParticipants)
        {
            try
            {
                foreach (var invitedParticipant in invitedParticipants)
                {
                    Invitee invitee = new Invitee(OrganiserID, invitedParticipant.id.ToString(), invitedParticipant.Important);
                    this._invitees.Add(invitee.InviteeID, invitee);
                    TotalInviteesUnanswered++;
                    TotalInvitees++;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        private void sendPropositionToInvitees()
        {
            //This will need to send the proposition to the right invitee
        }

        /// <summary>
        /// This will resolve the id of the creator
        /// </summary>
        /// <param name="userProfileId">The id of who requested the meeting
        /// </param>
        /// <returns></returns>
        private UserProfile resolveRequestedBy( string userProfileId)
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

        public Boolean IsTheMeetingReadyToGo()
        {
            /*
            if(this.LocalInviter.DidAllInviteesAccept())
            {
                return true;
            }*/
            return false;
        }

        public Boolean registerAvailabilityInvitee(InviterAnswerBindingModel model)
        {
            if (this._invitees.ContainsKey(model.InviteeID))
            {
                this._invitees[model.InviteeID].Answer = model.Answer;
                CheckAnswer(model.InviteeID);
            }

            //TODO:Should this be a new feature? HowManyCantCome()
            //Notify the Creator if to many non-imporant people can't come.
            //Let's say that if X% of the people did not accept it will send a notice to the creator if the meeting should continue
            //Or even another deadline

            return false;
        }

        private Boolean CheckAnswer(string id)
        {
            //TODO: If their is send a new proposition reduce Totals!
            if (this._invitees[id].Important == true && this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Reschudele meeting
                //Let the System know that it need to reschedule everything
                //But add a blackspot to the existing list

                TotalImporantInviteesAbsent++;
                TotalInviteesAbsent++;

                return false;
            }
            else if (this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Let the system decide if their is a new proposition needed.
                //If their is no new proposition the meeting will continue (unless he joins online)

                TotalInviteesAbsent++;

                return false;
            }
            else if ( this._invitees[id].Answer == Availability.Online )
            {
                //TODO:Proposition may not be online at the first try
                //Example: Could you be present at this meeting Present/Absent
                //When the person chose Absent they get another message 
                //from the system asking if they want to be present Online
                //Increase the OnlineCounter for this organisation. 
                TotalInviteesOnline++;
            }
            TotalInviteesUnanswered--;
            return true;
        }
    }

    public class InvitedParticipant
    {
        [Required]
        public int id { get; set; }

        [Required]
        public Boolean Important { get; set; }
    }
}