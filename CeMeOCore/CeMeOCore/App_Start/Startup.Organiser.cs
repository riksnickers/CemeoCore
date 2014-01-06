using CeMeOCore.Logic.Organiser;
using CeMeOCore.Logic.Spots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore
{
    public partial class Startup
    {
        public void ConfigureOrganiser()
        {
            OrganiserManagerFactory = () => new OrganiserManager();
            SpotManagerFactory = () => new SpotManager();
        }

        public static Func<OrganiserManager> OrganiserManagerFactory { get; set; }
        public static Func<SpotManager> SpotManagerFactory { get; set; }
    }
}