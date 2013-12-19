using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class AppointmentController : Controller
    {
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
            var App= _db.Appointments.Find(id);
            return View(App);
        }

        // GET: /Appointment/Create
        //
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Appointment/Create
        [HttpPost]
        public ActionResult Create(Appointment app)
        {
            try
            {
                if (app == null)
                {
                    throw new ArgumentNullException("item");
                }
                _db.Appointments.Add(app);
                _db.SaveChanges();
                return View(app);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Appointment app)
        {
            try
            {
                _db.Appointments.Remove(app);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
