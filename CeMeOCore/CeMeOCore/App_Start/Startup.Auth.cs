using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using CeMeOCore.Providers;
using CeMeOCore.Logic.MeetingOrganiser;

namespace CeMeOCore
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManagerFactory = () => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            InviterManagerFactory = () => new InviterManager();
            OrganiserManagerFactory = () => new OrganiserManager();

            RoleManager<IdentityRole> RoleManager = RoleManagerFactory();
            String administratorRole = "Administrator";
            if (!RoleManager.RoleExists(administratorRole))
            {
                var roleresult = RoleManager.Create(new IdentityRole(administratorRole));
            }

            UserManager<IdentityUser> UserManager = UserManagerFactory();
            
            String administratorUser = "Admin";
            String administratorPass = "superadmin";
            if( UserManager.FindByName(administratorUser) == null )
            {
                IdentityUser adminUser = new IdentityUser()
                {
                    UserName = administratorUser
                };
                IdentityResult ir = UserManager.Create(adminUser, administratorPass);
                UserManager.AddToRole(UserManager.FindByName(administratorUser).Id, administratorRole);
            }
           
            
            

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

        public static Func<RoleManager<IdentityRole>> RoleManagerFactory { get; set; }

        public static Func<InviterManager> InviterManagerFactory { get; set; }

        public static Func<OrganiserManager> OrganiserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}
