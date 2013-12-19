using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is an enumartion for the Availability options
    /// </summary>
    public enum Availability : int 
    {
        Present = 0,
        Absent = 1,
        Online = 2
    }
}