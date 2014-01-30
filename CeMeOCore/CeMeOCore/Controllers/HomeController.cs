using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult CheckLogin()
        {
            if (Request.Cookies["key"] != null)
            {
                var value = Request.Cookies["key"].Value;
            }
            //var cookie = null;
            return RedirectToAction("","",);
        }
    }
}
