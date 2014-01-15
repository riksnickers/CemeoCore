using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeMeOCore.Models;
using CeMeOCore.Logic.Range;

namespace CeMeOCore.Logic.Spots
{
    public interface ISpot
    {
        SpotBoolean isAvailable(DateTime value);
        SpotBoolean isAvailable(IRange<DateTime> range);

        DateRange DateRange { get; }
    }
}
