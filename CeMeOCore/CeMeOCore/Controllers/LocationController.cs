using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class LocationController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the Locations.";
            var model = _db.Locations;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var loc = _db.Locations.Find(id);
            return View(loc);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,Name,Street,Number,Zip,City,State,Country,Addition")] Location location)
        {
            if (ModelState.IsValid)
            {
                _db.Locations.Add(location);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = _db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,Name,Street,Number,Zip,City,State,Country,Addition")] Location location)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(location).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = _db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = _db.Locations.Find(id);
            _db.Locations.Remove(location);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
