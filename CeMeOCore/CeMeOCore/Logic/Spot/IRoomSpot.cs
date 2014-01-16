using CeMeOCore.Logic.Range;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// Interface for when a room is available in a spot
    /// </summary>
    public interface IRoomSpot
    {
        SpotBoolean isAvailable(Room room, DateTime value);
        SpotBoolean isAvailable(Room room, DateRange range);
    }
}