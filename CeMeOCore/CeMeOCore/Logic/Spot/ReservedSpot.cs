using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// A reserved spot, this is a spot that holds people and rooms for a specific daterange
    /// </summary>
    public class ReservedSpot : ISpot, IRoomSpot, IPersonSpot
    {
        /// <summary>
        /// The daterange
        /// </summary>
        private DateRange _dateRange;
        /// <summary>
        /// An id
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// The rooms
        /// </summary>
        private HashSet<Room> _rooms;
        /// <summary>
        /// The persons
        /// </summary>
        private HashSet<UserProfile> _persons;

        /// <summary>
        /// Constructor that initialize basic stuff
        /// </summary>
        public ReservedSpot()
        {
            this._dateRange = new DateRange(new DateTime(), new DateTime());
            this.Guid = Guid.NewGuid();
            this._rooms = new HashSet<Room>();
            this._persons = new HashSet<UserProfile>();
        }

        /// <summary>
        /// DateRange property
        /// </summary>
        public DateRange DateRange
        {
            get { return this._dateRange; }
            set { this._dateRange = value; }
        }

        /// <summary>
        /// Add a User to the list
        /// </summary>
        /// <param name="userProfile">The user to be added</param>
        public void Add(UserProfile userProfile)
        {
            this._persons.Add(userProfile);
        }

        /// <summary>
        /// Add a Room to the list
        /// </summary>
        /// <param name="room">The room to be added</param>
        public void Add(Room room)
        {
            this._rooms.Add(room);
        }

        /// <summary>
        /// NotImplemented
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(DateTime value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NotImplemented
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(IRange<DateTime> range)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NotImplemented, soon
        /// </summary>
        /// <param name="room"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(Room room, DateTime value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NotImplemented, soon
        /// </summary>
        /// <param name="room"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(Room room, DateRange range)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NotImplemented, soon
        /// </summary>
        /// <param name="user"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(UserProfile user, DateTime value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NotImplemented, soon
        /// </summary>
        /// <param name="user"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public SpotBoolean isAvailable(UserProfile user, DateRange range)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does this spot includes the room?
        /// </summary>
        /// <param name="room">The room we are looking for</param>
        /// <returns></returns>
        public Boolean Includes(Room room)
        {
            return this._rooms.Contains(room);
        }

        /// <summary>
        /// Does this spot includes the person
        /// </summary>
        /// <param name="userProfile">The droid, sorry person we are looking for</param>
        /// <returns></returns>
        public Boolean Includes(UserProfile userProfile)
        {
            return this._persons.Contains(userProfile);
        }
    }
}