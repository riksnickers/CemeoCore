using System;
using System.Collections.Generic;
using System.Linq;

namespace CeMeO.WPApp.Logic.Organiser
{
    /// <summary>
    /// This is an enumartion for the Availability options
    /// </summary>
    public enum Availability : int 
    {
        Unanswered = 0,
        Present = 1,
        Absent = 2,
        Online = 3
    }
}