using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class LocationController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /Location/
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the Locations.";
            var model = _db.Locations;
            return View(model);
        }

        //
        // GET: /Location/Details/5
        public ActionResult Details(int id)
        {
            var loc = _db.Locations.Find(id);
            return View(loc);
        }

        //
        // GET: /Location/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create
        [HttpPost]
        public ActionResult Create(Location loc)
        {
            try
            {
                if (loc == null)
                {
                    throw new ArgumentNullException("item");
                }
                _db.Locations.Add(loc);
                _db.SaveChanges();
                return View(loc);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Location/Edit/5
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
        // GET: /Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Location loc)
        {
            try
            {
                _db.Locations.Remove(loc);
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
