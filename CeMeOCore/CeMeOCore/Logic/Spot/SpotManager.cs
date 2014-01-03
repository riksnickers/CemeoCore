using CeMeOCore.Logic.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public class SpotManager
    {
        private SortedList<DateRange, PersonBlackSpot> _personSpots;
        private SortedList<DateRange, RoomBlackSpot> _roomSpots;
        private SortedList<DateRange, ReservedSpot> _reservedSpots;

        public SpotManager()
        {
            this._personSpots = new SortedList<DateRange, PersonBlackSpot>(new DateRange.Comparer());
            this._roomSpots = new SortedList<DateRange, RoomBlackSpot>(new DateRange.Comparer());
            this._reservedSpots = new SortedList<DateRange, ReservedSpot>(new DateRange.Comparer());
        }

        public SortedList<DateRange, PersonBlackSpot> GetPersonBlackSpots()
        {
            return this._personSpots;
        }

        public SortedList<DateRange, RoomBlackSpot> GetRoomBlackSpots()
        {
            return this._roomSpots;
        }

        public SortedList<DateRange, ReservedSpot> GetReservedSpots()
        {
            return this._reservedSpots;
        }

        //TODO:Write a test to test this
        public void AddSpot(ISpot spot)
        {
            if (spot is RoomBlackSpot)
            {
                this._roomSpots.Add(spot.DateRange, (RoomBlackSpot)spot);
            }
            else if (spot is PersonBlackSpot)
            {
                this._personSpots.Add(spot.DateRange, (PersonBlackSpot)spot);
            }
            else if (spot is ReservedSpot)
            {
                this._reservedSpots.Add(spot.DateRange, (ReservedSpot)spot);
            }
        }
    }
}