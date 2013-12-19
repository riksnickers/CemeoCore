using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is the manager that manages the Organiser instanties
    /// </summary>
    public class OrganiserManager
    {
        /// <summary>
        /// This dictionary holds all the Organiser instansions
        /// </summary>
        Dictionary<string, Organiser> dictionary;
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
        private Organiser getOrganiser(string organiserID)
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
        public Boolean addOrganiser(Organiser organiser)
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