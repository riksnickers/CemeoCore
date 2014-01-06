using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    /// <summary>
    /// This is the manager that manages the Organiser instanties
    /// </summary>
    public class OrganiserManager
    {
        /// <summary>
        /// This dictionary holds all the Organiser instansions
        /// </summary>
        private Dictionary<string, Organiser> dictionary;
        /// <summary>
        /// This is the constructor for OrganiserManager
        /// </summary>
        public OrganiserManager()
        {
            dictionary = new Dictionary<string, Organiser>();
        }

        /// <summary>
        /// With this method you can retrive a certain Organiser by providing the corresponding OrganiserID
        /// </summary>
        /// <param name="organiserID">This is the ID of the organiser</param>
        /// <returns>Inviter</returns>
        private Organiser GetOrganiser(string organiserID)
        {
            if (dictionary.ContainsKey(organiserID))
            {
                return dictionary[organiserID];
            }
            else return null;
        }

        /// <summary>
        /// With this method you can add a Organiser to the dictionary.
        /// </summary>
        /// <param name="organiser">The organiser object</param>
        /// <returns></returns>
        public Boolean AddOrganiser(Organiser organiser)
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

        public Organiser Create( ScheduleMeetingBindingModel model )
        {
            Organiser o = new Organiser(model.InvitedParticipants, model.BeforeDate, model.Dateindex, model.Creator, model.Duration);
            AddOrganiser(o);
            return o;
        }

        public OrganiserResponse GetOrganiserStatus(string organiserID)
        {
            //TODO: Get the status of the organiser
            return this.dictionary[organiserID].GetStatus();
        }

        /// <summary>
        /// With this method you can instanly register the answer of the Invitee to the organiser
        /// </summary>
        /// <param name="model">Passing the InviterAnswerBindingModel</param>
        /// <returns>Boolean</returns>
        public Boolean NotifyOrganiser(InviterAnswerBindingModel model)
        {
            return GetOrganiser(model.OrganiserID).registerAvailabilityInvitee(model);
        }
    }
}