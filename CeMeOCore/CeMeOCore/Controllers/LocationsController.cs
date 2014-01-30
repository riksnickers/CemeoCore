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
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(LocationsController));

        public LocationsController()
        {
            this._locationUoW = new LocationUoW();
        }

        /// Locations/Index
        /// <summary>
        /// The index function that takes care of the sorting, searching and paging
        /// </summary>
        /// <param name="sortorder"> order of sorting </param>
        /// <param name="currentfiler"> filter os sorting </param>
        /// <param name="currentfiler"> string for searching trhough teh records </param>
        /// <param name="page"> Paging parameter </param>
        /// <returns> a sorted and paged view with records from the locationtable</returns>
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

        /// Locations/Details/?
        /// <summary>
        /// This function will show you the details of a sepcific location
        /// </summary>
        /// <param name="id"> order of sorting </param>
        /// <returns> a view </returns>
        public ActionResult Details(int id)
        {
            Location detailedLocation = this._locationUoW.LocationRepository.dbSet.Find(id);
            return View(detailedLocation);
        }

        /// GET: /Locations/Create
        /// <summary>
        /// This function will show you a view to start creating a new location
        /// </summary>
        /// <returns> an empty view with an empty new location </returns>
        public ActionResult Create()
        {
            Location newLocation = new Location();
            return View(newLocation);
        }

        /// POST: /Locations/Create
        /// <summary>
        /// This function will handle the event
        /// </summary>
        /// <returns> creates a new location </returns>
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

        /// GET: /Locations/Edit/?
        /// <summary>
        /// This function will show you a view with filled in input boxes so you can edit a certain location
        /// </summary>
        /// <param name="id"> Specific location to edit </param>
        /// <returns> a view to edit the specific location </returns>
        public ActionResult Edit(int id)
        {
            var loca = this._locationUoW.LocationRepository.dbSet.Find(id);
            return View(loca);
        }

        /// POST: /Locations/Edit/?
        /// <summary>
        /// This function will handle the post event
        /// </summary>
        /// <param name="id"> Specific location to edit </param>
        /// <returns></returns>
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


        /// POST: /Locations/Delete/?
        /// <summary>
        /// This function will delete a location
        /// </summary>
        /// <param name="id"> Specific location to be deletete </param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                var Location = this._locationUoW.LocationRepository.dbSet.Find(id);
                return View(Location);
            }
            catch
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, Location toDel)
        {
            try
            {
                var original = this._locationUoW.LocationRepository.dbSet.Find(id);
                this._locationUoW.LocationRepository.dbSet.Remove(original);
                this._locationUoW.LocationRepository.context.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
