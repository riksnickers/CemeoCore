﻿using CeMeOCore.Models;
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

        /*
        public ActionResult Create()
        {
            var newLoc = new Location();

            return View(newLoc);
        }

        [HttpPost]
        public ActionResult Create(Location toAdd)
        {
            if (ModelState.IsValid)
            {
                var insert = new Location();
                insert.Name = toAdd.Name;
                insert.Street = toAdd.Street;
                insert.Number = toAdd.Number;
                insert.Zip = toAdd.Zip;
                insert.City = toAdd.City;
                insert.Country = toAdd.Country;
                insert.Addition = toAdd.Addition;
                insert.State = toAdd.State;

                _db.Locations.Add(insert);
                _db.SaveChanges();

                }
                else
                {
                    return View("Index");
                }
            return View("Index");
        }*/

        // GET: /LocationControllertest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LocationControllertest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /LocationControllertest/Edit/5
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

        // POST: /LocationControllertest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /LocationControllertest/Delete/5
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

        // POST: /LocationControllertest/Delete/5
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