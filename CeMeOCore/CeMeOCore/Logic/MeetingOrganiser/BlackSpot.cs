using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// A blackspot is a object that holds a begin time and end time.
    /// Between this two times it is not allow to plan a meeting.
    /// </summary>
    public class BlackSpot
    {
        /// <summary>
        /// This is the begin time of the blackspot
        /// </summary>
        private DateTime _beginTime;
        /// <summary>
        /// This is the end time of the blackspot
        /// </summary>
        private DateTime _endTime;

        /// <summary>
        /// Blackspot constructor
        /// </summary>
        /// <param name="beginTime">the begin time of the blackspot</param>
        /// <param name="duration">How long the appointment takes</param>
        public BlackSpot( DateTime beginTime, int duration )
        {
            this._beginTime = beginTime;
            //Calculate endTime
        }
        
        /// <summary>
        /// Blackspot constructor
        /// </summary>
        /// <param name="beginTime">the begin time of the blackspot</param>
        /// <param name="endTime">the end time of the blackspot</param>
        public BlackSpot( DateTime beginTime, DateTime endTime)
        {
            this._beginTime = beginTime;
            this._endTime = endTime;
        }

        /// <summary>
        /// With this method you can ask the blackspot if a certain time does not conflict with the blackspot.
        /// </summary>
        /// <param name="thisTime">The time you want to test</param>
        /// <returns>Boolean</returns>
        public Boolean isAvailable( DateTime thisTime )
        {
            if( (thisTime >= this._beginTime)
                && (thisTime <= this._endTime) )
            {
                return false;
            }

            return true;
        }
    }
}