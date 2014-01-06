using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Range
{
    /// <summary>
    /// The DateRange class holds a start DateTime and a end DateTime.
    /// </summary>
    public class DateRange : IRange<DateTime>, IEquatable<DateRange>
    {
        private Guid _guid;
        /// <summary>
        /// private holder for the start datetime.
        /// </summary>
        private DateTime _start;
        /// <summary>
        /// private holder for the end datetime
        /// </summary>
        private DateTime _end;
        /// <summary>
        /// The constructor for Daterange
        /// </summary>
        /// <param name="start">The begin DateTime (earliest datetime)</param>
        /// <param name="end">The end DateTime (latest datetime)</param>
        public DateRange(DateTime start, DateTime end)
        {
            this._guid = Guid.NewGuid();
            this.Start = start;
            this.End = end;
        }

        public DateRange(DateTime start) : this(start, start) { }

        /// <summary>
        /// The public getter for Start
        /// </summary>
        public DateTime Start
        {
            get { return this._start; }
            protected set { this._start = value; }
        }

        /// <summary>
        /// The public getter for End
        /// </summary>
        public DateTime End
        {
            get { return this._end; }
            protected set { this._end = value; }
        }

        /// <summary>
        /// Does the range include this value?
        /// </summary>
        /// <param name="value">A DateTime value</param>
        /// <returns>True or False</returns>
        public bool Includes(DateTime value)
        {
            return (Start >= value) && (value <= End);
        }

        /// <summary>
        /// Does the range includes a specific range?
        /// </summary>
        /// <param name="range">A DateTime range value</param>
        /// <returns>True or False</returns>
        public bool Includes(IRange<DateTime> range)
        {
            return (Start >= range.Start) && (range.End <= End);
        }

        public class Comparer : IComparer<DateRange>
        {
            public int Compare(DateRange x, DateRange y)
            {
                if (x == null || y == null)
                    throw new InvalidOperationException("both of parameters must be not null");

                int res = x.Start.CompareTo(y.Start);
                if (res == 0)
                {
                    res = x._guid.CompareTo(y._guid);
                }
                return res;
            }
        }

        public bool Equals(DateRange other)
        {
            if (this.Start == other.Start && this.End == other.End)
            {
                return true;
            }
            else return false;
        }

    }
}