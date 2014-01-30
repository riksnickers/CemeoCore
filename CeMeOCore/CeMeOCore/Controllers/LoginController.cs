using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web.Http.ModelBinding;
using System.Security.Cryptography;
using System.Data.Entity.Validation;
using CeMeOCore.DAL.Models;
using CeMeOCore.Providers;
using CeMeOCore.Results;
using CeMeOCore.DAL.UnitsOfWork;
using log4net;
using Microsoft.Owin.Host.SystemWeb;


namespace CeMeOCore.Controllers
{
    public class LoginController : Controller
    {
        private const string LocalLoginProvider = "Local";
        private UserUoW _userUoW;
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult CheckLogin(LoginModel log)
        {

            // Default UserStore constructor uses the default connection string named: DefaultConnection
           *var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser() { UserName = log.UserName };

            IdentityResult result = manager.Create(user, log.Password);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                Response.Redirect("~/Login.aspx");
            }
            else
            {
               // StatusMessage.Text = result.Errors.FirstOrDefault();
            }
    }*/
	}
}