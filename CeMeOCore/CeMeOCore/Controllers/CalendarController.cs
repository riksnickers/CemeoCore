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

        //
        // GET: /Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Calendar/Create
        [HttpPost]
        public ActionResult Create(Calendar cal)
        {
            try
            {
                if (cal == null)
                {
                    throw new ArgumentNullException("item");
                }
                _db.Calendars.Add(cal);
                _db.SaveChanges();
                return View(cal);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Calendar cal)
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
        // GET: /Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Calendar cal)
        {
            try
            {
                _db.Calendars.Remove(cal);
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
