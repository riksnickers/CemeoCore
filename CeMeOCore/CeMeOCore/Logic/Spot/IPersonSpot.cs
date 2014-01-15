using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public interface IPersonSpot
    {
        SpotBoolean isAvailable(UserProfile user, DateTime value);
        SpotBoolean isAvailable(UserProfile user, DateRange range);
    }
}