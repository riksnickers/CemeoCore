using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public class RoomBlackSpot : BlackSpot, IRoomSpot
    {
        private Room _room;

        public RoomBlackSpot(DateTime beginTime, DateTime endTime, Room room)
            : base(beginTime, endTime)
        {
            this._room = room;
        }

        public RoomBlackSpot()
            : base(new DateTime(), new DateTime())
        {
            //Do nothing
        }

        public Room Person
        {
            get { return this._room; }
        }

        public SpotBoolean isAvailable(Room room, DateTime value)
        {
            if (this._room == room)
                return SpotBoolean.Wrong;
            else
                return isAvailable(value);
        }

        public SpotBoolean isAvailable(Room room, DateRange range)
        {
            if (this._room == room)
                return SpotBoolean.Wrong;
            else
                return isAvailable(range);
        }
    }
}