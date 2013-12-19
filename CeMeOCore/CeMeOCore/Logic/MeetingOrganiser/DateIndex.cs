using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.MeetingOrganiser
{
    /// <summary>
    /// This is an enumartion for the DataIndex options
    /// </summary>
    public enum DateIndex
    {
        Today = 0,
        WithinThisWorkWeek = 1,
        Within7Days = 2,
        WithinThisMonth = 3,
        Within30Days = 4,
        BeforeADate = 5
    }
}