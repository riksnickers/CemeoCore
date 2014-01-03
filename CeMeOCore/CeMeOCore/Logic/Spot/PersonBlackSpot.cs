using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public class PersonBlackSpot : BlackSpot, IPersonSpot
    {
        private UserProfile _person;

        public PersonBlackSpot(DateTime beginTime, DateTime endTime, UserProfile person)
            : base(beginTime, endTime)
        {
            this._person = person;
        }

        public PersonBlackSpot()
            : base(new DateTime(), new DateTime())
        {
            //Do nothing
        }
        public UserProfile Person
        {
            get { return this._person; }
        }

        public SpotBoolean isAvailable(UserProfile user, DateTime value)
        {
            if (this._person != user)
                return SpotBoolean.Wrong;
            else
                return isAvailable(value);
        }

        public SpotBoolean isAvailable(UserProfile user, DateRange range)
        {
            if (this._person == user)
                return SpotBoolean.Wrong;
            else
                return isAvailable(range);
        }
    }
}