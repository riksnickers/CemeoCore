using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CeMeOCore.Models;
using System.ComponentModel.DataAnnotations;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is the Inviter class, this will manage all incomming reponses from the intvitees.
    /// It will also invite all attendees.
    /// </summary>
    public class Inviter : IInviter
    {
        /// <summary>
        /// The id of the InviterID process that will be used.
        /// </summary>
        public string InviterID { get; private set; }
        /// <summary>
        /// All attendees for this process (private holder)
        /// </summary>
        private List<UserProfile> _attendees;
        /// <summary>
        /// All attendees for this process (public accessor)
        /// </summary>
        public List<UserProfile> Attendees
        {
            get {
                return _attendees;
            }
        }
        /// <summary>
        /// This method will add a UserProfile to the list.
        /// </summary>
        /// <param name="up">UserProfile object</param>
        /// <returns></returns>
        public Boolean addAttendee( UserProfile up )
        {
            try
            {
                this._attendees.Add(up);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method will send a push notification to all attendees
        /// </summary>
        /// <returns></returns>
        public Boolean sendProposition()
        {

            return false;
        }

        /// <summary>
        /// This method will register the availability status for an attendee
        /// </summary>
        /// <param name="inviteeId">Who is invited</param>
        /// <param name="option">what is his/her response</param>
        /// <returns></returns>
        public Boolean registerAvailabilityAttendee(string inviteeId, Availability option)
        {
            return false;
        }
    }

    /// <summary>
    /// This is the BindingModel for when an answer is posted to the api.
    /// </summary>
    public class InviterAnswerBindingModel
    {
        [Required]
        public string InviterID { get; set; }
        [Required]
        public string InviteeID { get; set; }
        [Required]
        public Availability Answer { get; set; }
    }
}