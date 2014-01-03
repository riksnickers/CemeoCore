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
        public ReservedSpot()
        {
            this._dateRange = new DateRange(new DateTime(), new DateTime());
        }

        private DateRange _dateRange;
        public DateRange DateRange
        {
            get { return this._dateRange; }
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
    }
}