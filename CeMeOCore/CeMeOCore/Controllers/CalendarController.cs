using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class CalendarController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /Calendar/
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the calendars.";
            var cal = _db.Calendars;
            return View(cal);
        }

        //
        // GET: /Calendar/Details/5
        public ActionResult Details(int id)
        {
            var cal = _db.Calendars.Find(id);
            return View(cal);
        }
    }
}
