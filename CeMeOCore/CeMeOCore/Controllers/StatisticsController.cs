using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/
        private CeMeoContext _db = new CeMeoContext();
        public ActionResult Index()
        {
            int countMeetings = _db.Meetings.Count();
            int countLocations = _db.Locations.Count();
            int countUsers = _db.Users.Count();
            int countRooms = _db.Rooms.Count();
            
            return View();
        }

        //
        // GET: /Statistics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
