using CeMeOCore.DAL.UnitsOfWork;
using System.Web.Mvc;


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