using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the appointments.";
            var model = _db.Appointments;
            return View(model);

        }

        //
        // GET: /Appointment/Details/5
        public ActionResult Details(int id)
        {
            var App = _db.Appointments.Find(id);
            return View(App);
        }
    }
}
