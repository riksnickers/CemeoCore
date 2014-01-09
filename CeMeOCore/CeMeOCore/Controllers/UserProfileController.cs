using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class UserProfileController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /UserProfile/
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the user.";
            var model = _db.Users;
            return View(model);
        }

        //
        // GET: /UserProfile/Details/5
        public ActionResult Details(int id)
        {
            var usr = _db.Users.Find(id);
            return View(usr);
        }
    }
}
