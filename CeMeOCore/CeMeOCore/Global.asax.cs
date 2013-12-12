using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CeMeOCore
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            /*
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            //Controle of de roles al bestaan, bestaan deze nog niet dan worden deze meteen aangemaakt
            //Administrator
            if (!Roles.RoleExists("Administration"))
            {
                Roles.CreateRole("Administration");
            }
            //Developer voor de programmeurs
            if (!Roles.RoleExists("Developer"))
            {
                Roles.CreateRole("Developer");
            }

            //Controle of er al een gebruiker bestaat met deze rol, anders wordt deze meteen aangemaakt
            if (Membership.GetUser("Administrator", false) == null)
            {
                WebSecurity.CreateUserAndAccount("Administrator", "Administrator12345CeMeo!");
            }
            if (Membership.GetUser("TomBoffe", false) == null)
            {
                WebSecurity.CreateUserAndAccount("TomBoffe", "Developer12345CeMeo!");
            }
            if (Membership.GetUser("JeffreySmets", false) == null)
            {
                WebSecurity.CreateUserAndAccount("JeffreySmets", "Developer12345CeMeo!");
            }
            if (Membership.GetUser("TomBoffe", false) == null)
            {
                WebSecurity.CreateUserAndAccount("JordyBaldewijns", "Developer12345CeMeo!");
            }
            if (Membership.GetUser("TomBoffe", false) == null)
            {
                WebSecurity.CreateUserAndAccount("TychaBuekers", "Developer12345CeMeo!");
            }

            //Hierbij wordt er gecontroleerd of er een gebruiker bestaat met bdeze rol, als dit niet het geval is, wordt er een gebruiker aangewezen die deze rol zal krijgen
            if (!Roles.GetRolesForUser("Administrator").Contains("Administration"))
            {
                Roles.AddUsersToRoles(new[] { "Administrator" }, new[] { "Administration" });
            }
            //Role aan het account van Tom Boffé toevoegen
            if (!Roles.GetRolesForUser("TomBoffe").Contains("Developer"))
            {
                Roles.AddUsersToRoles(new[] { "TomBoffe" }, new[] { "Developer" });
            }
            //Role aan het account van Jeffrey Smets toevoegen
            if (!Roles.GetRolesForUser("JeffreySmets").Contains("Developer"))
            {
                Roles.AddUsersToRoles(new[] { "JeffreySmets" }, new[] { "Developer" });
            }
            //Role aan het account van Jordy Baldewijns toevoegen
            if (!Roles.GetRolesForUser("JordyBaldewijns").Contains("Developer"))
            {
                Roles.AddUsersToRoles(new[] { "JordyBaldewijns" }, new[] { "Developer" });
            }
            //Role aan het account van Tycha Buekers toevoegen
            if (!Roles.GetRolesForUser("TychaBuekers").Contains("Developer"))
            {
                Roles.AddUsersToRoles(new[] { "TychaBuekers" }, new[] { "Developer" });
            }
            */
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
