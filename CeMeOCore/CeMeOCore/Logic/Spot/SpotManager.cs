using CeMeOCore.Logic.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public class SpotManager
    {
        //private SortedList<DateRange, PersonBlackSpot> _personSpots;
        private Dictionary<String, SortedList<DateRange, PersonBlackSpot>> _organiserPersonSpots;
        private SortedList<DateRange, RoomBlackSpot> _roomSpots;
        private SortedList<DateRange, ReservedSpot> _reservedSpots;

        public SpotManager()
        {
            //this._personSpots = new SortedList<DateRange, PersonBlackSpot>(new DateRange.Comparer());
            this._organiserPersonSpots = new Dictionary<string, SortedList<DateRange, PersonBlackSpot>>();
            this._roomSpots = new SortedList<DateRange, RoomBlackSpot>(new DateRange.Comparer());
            this._reservedSpots = new SortedList<DateRange, ReservedSpot>(new DateRange.Comparer());
        }

        public SortedList<DateRange, PersonBlackSpot> GetPersonBlackSpots(string OrganiserID)
        {
            return this._organiserPersonSpots[OrganiserID];
        }

        public SortedList<DateRange, RoomBlackSpot> GetRoomBlackSpots()
        {
            return this._roomSpots;
        }

        public SortedList<DateRange, ReservedSpot> GetReservedSpots()
        {
            return this._reservedSpots;
        }

        public void AddSpot(ISpot spot)
        {
            try
            {
                if (spot is RoomBlackSpot)
                {
                    this._roomSpots.Add(spot.DateRange, (RoomBlackSpot)spot);
                }
                else if (spot is PersonBlackSpot)
                {
                    //Convert spot to PersonBlackSpot so that the OrganiserID can be accessed
                    PersonBlackSpot pbs = (PersonBlackSpot)spot;
                    //Check if the OrganiserID is already a key of the OrganiserPersonSpots
                    if ( !this._organiserPersonSpots.ContainsKey(pbs.OrganiserID) )
                    {
                        //If not create a new SortedList for the organiser and add it to the dictionary
                        this._organiserPersonSpots.Add(pbs.OrganiserID, new SortedList<DateRange, PersonBlackSpot>(new DateRange.Comparer()));
                    }
                    //Now add the PersonBlackSpot to the correct list.
                    this._organiserPersonSpots[pbs.OrganiserID].Add(spot.DateRange, pbs);
                }
                else if (spot is ReservedSpot)
                {
                    this._reservedSpots.Add(spot.DateRange, (ReservedSpot)spot);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}