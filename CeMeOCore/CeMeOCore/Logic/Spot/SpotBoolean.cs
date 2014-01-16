using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// A extended Boolean
    /// Yes = True
    /// No = False
    /// Wrong = Definitly False..
    /// </summary>
    public enum SpotBoolean : int
    {
        Wrong = 0,
        Yes = 1,
        No = 2
    }
}