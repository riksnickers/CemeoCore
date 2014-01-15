using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public class ReservedSpot : ISpot, IRoomSpot, IPersonSpot
    {
        private DateRange _dateRange;
        public Guid Guid { get; set; }
        private HashSet<Room> _rooms;
        private HashSet<UserProfile> _persons;

        public ReservedSpot()
        {
            this._dateRange = new DateRange(new DateTime(), new DateTime());
            this.Guid = Guid.NewGuid();
            this._rooms = new HashSet<Room>();
            this._persons = new HashSet<UserProfile>();
        }

        public DateRange DateRange
        {
            get { return this._dateRange; }
            set { this._dateRange = value; }
        }

        public void Add(UserProfile userProfile)
        {
            this._persons.Add(userProfile);
        }

        public void Add(Room room)
        {
            this._rooms.Add(room);
        }

        public SpotBoolean isAvailable(DateTime value)
        {
            throw new NotImplementedException();
        }

        public SpotBoolean isAvailable(IRange<DateTime> range)
        {
            throw new NotImplementedException();
        }

        public SpotBoolean isAvailable(Room room, DateTime value)
        {
            throw new NotImplementedException();
        }

        public SpotBoolean isAvailable(Room room, DateRange range)
        {
            throw new NotImplementedException();
        }

        public SpotBoolean isAvailable(UserProfile user, DateTime value)
        {
            throw new NotImplementedException();
        }

        public SpotBoolean isAvailable(UserProfile user, DateRange range)
        {
            throw new NotImplementedException();
        }

        public Boolean Includes(Room room)
        {
            return this._rooms.Contains(room);
        }

        public Boolean Includes(UserProfile userProfile)
        {
            return this._persons.Contains(userProfile);
        }
    }
}