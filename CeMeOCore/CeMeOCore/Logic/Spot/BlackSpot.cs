using CeMeOCore.Logic.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// A blackspot is a object that holds a DateRange.
    /// Inside this range it is not allowed to plan a meeting.
    /// </summary>
    public abstract class BlackSpot : ISpot
    {
        /// <summary>
        /// This is the begin time of the blackspot
        /// </summary>
        private DateRange _dateRange;

        /// <summary>
        /// Blackspot constructor
        /// </summary>
        /// <param name="beginTime">the begin time of the blackspot</param>
        /// <param name="endTime">the end time of the blackspot</param>
        protected BlackSpot(DateTime beginTime, DateTime endTime)
        {
            this._dateRange = new DateRange(beginTime, endTime);
        }

        /// <summary>
        /// Public get of beginTime
        /// </summary>
        public DateTime BegintTime
        {
            get { return this._dateRange.Start; }
        }

        /// <summary>
        /// Public get of endTime
        /// </summary>
        public DateTime EndTime
        {
            get { return this._dateRange.End; }
        }

        public DateRange DateRange
        {
            get { return this._dateRange; }
        }

        /// <summary>
        /// With this method you can ask the blackspot if a certain time does not conflict with the blackspot.
        /// </summary>
        /// <param name="thisTime">The time you want to test</param>
        /// <returns>Boolean</returns
        public SpotBoolean isAvailable(DateTime value)
        {
            if (this._dateRange.Includes(value))
                return SpotBoolean.Yes;
            else
                return SpotBoolean.No;
        }

        public SpotBoolean isAvailable(IRange<DateTime> range)
        {
            if (this._dateRange.Includes(range))
                return SpotBoolean.Yes;
            else
                return SpotBoolean.No;
        }

    }
}