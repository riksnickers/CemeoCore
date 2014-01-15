using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// The personBlackSpot extends a blackspot and implement the IPersonSpot
    /// </summary>
    public class PersonBlackSpot : BlackSpot, IPersonSpot
    {
        /// <summary>
        /// The person for this blackspot
        /// </summary>
        private UserProfile _person;

        //TODO: This need to be rethinked..
        /// <summary>
        /// hmmm
        /// </summary>
        private string _organiserID;

        public string OrganiserID
        {
            get { return _organiserID; }
        }
        
        /// <summary>
        /// The constructor for the PersonBlackSpot
        /// </summary>
        /// <param name="beginTime">begin dateTime</param>
        /// <param name="endTime">end dateTime</param>
        /// <param name="person">Who</param>
        /// <param name="organiserID">...</param>
        public PersonBlackSpot(DateTime beginTime, DateTime endTime, UserProfile person, string organiserID)
            : base(beginTime, endTime)
        {
            this._person = person;
            this._organiserID = organiserID;
        }

        /// <summary>
        /// Empty constructor ('mock')
        /// </summary>
        public PersonBlackSpot()
            : base(new DateTime(), new DateTime())
        {
            //Do nothing
        }
        /// <summary>
        /// Property to get the person
        /// </summary>
        public UserProfile Person
        {
            get { return this._person; }
        }

        /// <summary>
        /// Is a person availble for a datetime
        /// </summary>
        /// <param name="user">The user we are looking for</param>
        /// <param name="value">The datetime</param>
        /// <returns></returns>
        public SpotBoolean isAvailable(UserProfile user, DateTime value)
        {
            if (this._person != user)
                return SpotBoolean.Wrong;
            else
                return isAvailable(value);
        }

        /// <summary>
        /// Is a person available for a dateRange
        /// </summary>
        /// <param name="user">The user we are looking for</param>
        /// <param name="range">the daterange</param>
        /// <returns></returns>
        public SpotBoolean isAvailable(UserProfile user, DateRange range)
        {
            if (this._person == user)
                return SpotBoolean.Wrong;
            else
                return isAvailable(range);
        }
    }
}