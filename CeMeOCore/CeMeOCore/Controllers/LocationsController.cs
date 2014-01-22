using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.DAL.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CeMeOCore.DAL.Context;

namespace CeMeOCore.Controllers
{
    public class LocationsController : Controller
    {
        //
        // GET: /Locations/
        private LocationUoW _locationUoW;

        public LocationsController()
        {
            this._locationUoW = new LocationUoW();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Title of the page
            ViewBag.Title = "Overview of all the Locations.";

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.StreetSortParm = sortOrder == "Street" ? "Street" : "Street";
            ViewBag.NumberSortParm = sortOrder == "Number" ? "Number" : "Number";
            ViewBag.ZipSortParm = sortOrder == "Zip" ? "Zip" : "Zip";
            ViewBag.CitySortParm = sortOrder == "City" ? "City" : "City";
            ViewBag.StateSortParm = sortOrder == "State" ? "State" : "State";
            ViewBag.ZipSortParm = sortOrder == "Zip" ? "Zip" : "Zip";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country" : "Country";
            ViewBag.AdditionSortParm = sortOrder == "Addition" ? "Addition" : "Addition";

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

            // var locs = from s in _db.Locations select s;
            var locs = from s in this._locationUoW.LocationRepository.Get() select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                locs = locs.Where(s => s.Name.Contains(searchString));
                
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
                case "Zip":
                    locs = locs.OrderByDescending(s => s.Zip);
                    break;
                case "City":
                    locs = locs.OrderByDescending(s => s.City);
                    break;
                case "State":
                    locs = locs.OrderByDescending(s => s.State);
                    break;
                case "Country":
                    locs = locs.OrderByDescending(s => s.Country);
                    break;
                case "Addition":
                    locs = locs.OrderByDescending(s => s.Addition);
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

        //
        // GET: /Locations/Details/5
        public ActionResult Details(int id)
        {
            Location detailedLocation = this._locationUoW.LocationRepository.dbSet.Find(id);
            return View(detailedLocation);
        }

        //
        // GET: /Locations/Create
        public ActionResult Create()
        {
            Location newLocation = new Location();
            return View(newLocation);
        }

        //
        // POST: /Locations/Create
        [HttpPost]
        public ActionResult Create(Location newLocation)
        {
            try
            {
                Location newLocationToAdd = new Location();
                newLocationToAdd.Name = newLocation.Name;
                newLocationToAdd.Street = newLocation.Street;
                newLocationToAdd.Number = newLocation.Number;
                newLocationToAdd.Zip = newLocation.Zip;
                newLocationToAdd.City = newLocation.City;
                newLocationToAdd.Country = newLocation.Country;
                newLocationToAdd.State = newLocation.State;
                newLocationToAdd.Addition = newLocation.Addition;
                this._locationUoW.LocationRepository.dbSet.Add(newLocation);
                this._locationUoW.LocationRepository.context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Locations/Edit/5

        public ActionResult Edit(int id)
        {
            var loca = this._locationUoW.LocationRepository.dbSet.Find(id);
            return View(loca);
        }

        //
        // POST: /Locations/Edit/5
        [HttpPost]
        public ActionResult Edit(Location loc)
        {
            try
            {
                // TODO: Add update logic here
                var loca = this._locationUoW.LocationRepository.dbSet.Find(loc.LocationID);
                loca.Name = loc.Name;
                loca.Street = loc.Street;
                loca.Number = loc.Number;
                loca.Zip = loc.Zip;
                loca.City = loc.City;
                loca.Country = loc.Country;
                loca.State = loc.State;
                loca.Addition = loc.Addition;
                this._locationUoW.LocationRepository.context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Locations/Delete/5
        public void DeleteLocation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var original = this._locationUoW.LocationRepository.dbSet.Find(id);
                this._locationUoW.LocationRepository.dbSet.Remove(original);
                this._locationUoW.LocationRepository.context.SaveChanges();
                RedirectToAction("Index");
            }
            catch
            {
                RedirectToAction("Details");
            }
        }
    }
}
