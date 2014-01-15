using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Security;

[assembly: OwinStartup(typeof(CeMeOCore.Startup))]

namespace CeMeOCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureOrganiser();

            //TODO: PushSharp
            //http://www.codeproject.com/Tips/666989/Using-PushSharp-to-Send-Notifications-to-iOS-and-A
        }
    }
}
