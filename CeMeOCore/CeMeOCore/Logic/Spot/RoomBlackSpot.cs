using CeMeOCore.Logic.Range;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// The RoomBlackSpot extends a blackspot and implement the IPersonSpot
    /// </summary>
    public class RoomBlackSpot : BlackSpot, IRoomSpot
    {
        /// <summary>
        /// The Room for this blackspot 
        /// </summary>
        private Room _room;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginTime">the begin time</param>
        /// <param name="endTime">the end time</param>
        /// <param name="room">the room for what this spot is for</param>
        public RoomBlackSpot(DateTime beginTime, DateTime endTime, Room room)
            : base(beginTime, endTime)
        {
            this._room = room;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public RoomBlackSpot()
            : base(new DateTime(), new DateTime())
        {
            //Do nothing
        }

        /// <summary>
        /// Property for the room
        /// </summary>
        public Room Room
        {
            get { return this._room; }
        }

        /// <summary>
        /// Is a room availble for a datetime
        /// </summary>
        /// <param name="user">The room we are looking for</param>
        /// <param name="value">The datetime</param>
        /// <returns></returns>
        public SpotBoolean isAvailable(Room room, DateTime value)
        {
            if (this._room == room)
                return SpotBoolean.Wrong;
            else
                return isAvailable(value);
        }

        /// <summary>
        /// Is a room availble for a daterange
        /// </summary>
        /// <param name="user">The room we are looking for</param>
        /// <param name="value">The daterange</param>
        /// <returns></returns>
        public SpotBoolean isAvailable(Room room, DateRange range)
        {
            if (this._room == room)
                return SpotBoolean.Wrong;
            else
                return isAvailable(range);
        }
    }
}