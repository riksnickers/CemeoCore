using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is the manager that manages the Inviter instanties
    /// </summary>
    public class InviterManager
    {
        /// <summary>
        /// This dictionary holds all the Invite instansions
        /// </summary>
        Dictionary<string, Inviter> dictionary;
        /// <summary>
        /// This is the construction for InviteManager
        /// </summary>
        public InviterManager()
        {
            dictionary = new Dictionary<string, Inviter>();
        }

        /// <summary>
        /// With this method you can retrive a certain Inviter by providing the corresponding InviterID
        /// </summary>
        /// <param name="inviterID">This is the ID of the inviter</param>
        /// <returns>Inviter</returns>
        public Inviter getInviter(string inviterID)
        {
            if (dictionary.ContainsKey(inviterID))
            {
                return dictionary[inviterID];
            }
            else return null;
        }
        /// <summary>
        /// With this method you can add Inviters to the dictonary.
        /// </summary>
        /// <param name="inviter"></param>
        /// <returns>Boolean</returns>
        public Boolean addInviter(Inviter inviter)
        {
            try
            {
                dictionary.Add(inviter.InviterID, inviter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// With this method you can create an inviter and automaticly add it to the dictonary
        /// </summary>
        /// <returns></returns>
        public Inviter create()
        {
            Inviter i = new Inviter();
            addInviter(i);
            ; return i;
        }

        /// <summary>
        /// With this method you can instanly access the coressponding inviter instance method registerAvailabilityAttendee.
        /// </summary>
        /// <param name="inviterID">Which inviter instance you want to access</param>
        /// <param name="attendeeId">Which attendee you want to do the option for</param>
        /// <param name="option">Which option the attendee has chosen</param>
        /// <returns>Boolean</returns>
        public Boolean notifyInviter(string inviterID, string attendeeId, Availability option)
        {
            return getInviter(inviterID).registerAvailabilityAttendee(attendeeId, option);
        }
    }
}