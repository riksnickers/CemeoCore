using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CeMeOCore.Controllers
{
    public class AppointmentController : Controller
    {
        //Appointments are synced with exchange so we don't have to 
        //edit them and don't create 
        //any new appointments here, this has to be done in exchange

        private CeMeoContext _db = new CeMeoContext();

        //
        // GET: /Appointment/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Takes care of the sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Description" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description" : "Description";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.BeginTimeSortParm = sortOrder == "BeginTime" ? "BeginTime" : "BeginTime";
            ViewBag.EndTimeSortParm = sortOrder == "EndTime" ? "EndTime" : "EndTime";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "Location" : "Location";

            //paging and sorting
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var apps = from s in _db.Appointments select s;

            //control if there is a search
            if (!String.IsNullOrEmpty(searchString))
            {
                apps = apps.Where(s => s.Description.ToUpper().Contains(searchString.ToUpper()));
            }

            //sorting
            switch (sortOrder)
            {
                case "Description":
                    apps = apps.OrderByDescending(s => s.Description);
                    break;
                case "Date":
                    apps = apps.OrderBy(s => s.Date);
                    break;
                case "BeginTime":
                    apps = apps.OrderByDescending(s => s.BeginTime);
                    break;
                case "EndTime":
                    apps = apps.OrderByDescending(s => s.EndTime);
                    break;
                case "Location":
                    apps = apps.OrderByDescending(s => s.Location);
                    break;
                default:
                    apps = apps.OrderBy(s => s.Description);
                    break;
            }

            //paging
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(apps.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Appointment/Details/5
        public ActionResult Details(int id)
        {
            //Shows the details of a specific appointment
            var App = _db.Appointments.Find(id);
            return View(App);
        }

        //
        // GET: /Appointment/Details/5
        public ActionResult Try()
        {
            return View();
        }
    }
}
