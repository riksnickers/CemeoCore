using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CeMeOCore.Controllers
{
    public class LocationController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Title of the page
            ViewBag.Title = "Overview of all the Locations.";

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.StreetSortParm = sortOrder == "Street" ? "Street" : "Street";
            ViewBag.NumberSortParm = sortOrder == "Number" ? "Number" : "Number";
            ViewBag.ZipSortParm = sortOrder == "City" ? "City" : "City";
            ViewBag.CitySortParm = sortOrder == "Zip" ? "Zip" : "Zip";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country" : "Country";
            ViewBag.StateSortParm = sortOrder == "State" ? "State" : "State";

            //Paging
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //End paging

            var locs = from s in _db.Locations select s;
            
            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                locs = locs.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            //End searching

            switch (sortOrder)
            {
                case "Name":
                    locs = locs.OrderByDescending(s => s.Name);
                    break;
                case "Street":
                    locs = locs.OrderBy(s => s.Street);
                    break;
                case "Number":
                    locs = locs.OrderByDescending(s => s.Number);
                    break;
                case "City":
                    locs = locs.OrderByDescending(s => s.City);
                    break;
                case "Zip":
                    locs = locs.OrderByDescending(s => s.Zip);
                    break;
                case "Country":
                    locs = locs.OrderByDescending(s => s.Country);
                    break;
                case "State":
                    locs = locs.OrderByDescending(s => s.State);
                    break;
                default:
                    locs = locs.OrderBy(s => s.Name);
                    break;
            }

            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(locs.ToPagedList(pageNumber, pageSize));
            //End sorting
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
