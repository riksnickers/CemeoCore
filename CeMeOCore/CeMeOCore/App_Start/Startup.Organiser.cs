using CeMeOCore.Logic.Organiser;
using CeMeOCore.Logic.Spots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore
{
    /// <summary>
    /// This is another part of the Startup class containing all the elements needed for the organiser
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Method for configuring the organiser manager and rest.
        /// </summary>
        public void ConfigureOrganiser()
        {
            OrganiserManager.Configure();
            SpotManagerFactory = new SpotManager();
        }

        public static SpotManager SpotManagerFactory { get; set; }

        //public static WorkSchedule WorkSchedule { get; set; }
    }
}