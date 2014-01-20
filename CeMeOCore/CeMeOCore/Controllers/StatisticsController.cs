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
        private Statistics stats = new Statistics();

        public ActionResult Index()
        {
            //Count statistics
            stats.countMeetings = _db.Meetings.Count();
            stats.countLocations = _db.Locations.Count();
            stats.countUsers = _db.Users.Count();
            stats.countRooms = _db.Rooms.Count();

            //Meeting statistics
            //Statistics to get the number of meeting in the forenoon and the afternoon and put them in a pie chart.
           // int countMeetingsForenoon = from n in _db.Meetings where n.
           // int countMeetingAfternoon = _db.Meetings.Count();

            //Location statistics


            //Room statisctics


            //Users statistics
            //Statistics to get the user with the most organized meetings
           // var countUsersWithMostMeetings = from s in _db.Users where 


            return View(stats);
        }

        //
        // GET: /Statistics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
