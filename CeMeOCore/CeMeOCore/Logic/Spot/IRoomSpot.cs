using CeMeOCore.Logic.Range;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    public interface IRoomSpot
    {
        SpotBoolean isAvailable(Room room, DateTime value);
        SpotBoolean isAvailable(Room room, DateRange range);
    }
}