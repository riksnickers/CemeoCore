using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CeMeOCore.Controllers
{
    public class LocationsController : Controller
    {
        //
        // GET: /Locations/
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

            var locs = from s in _db.Locations select s;

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
            return View();
        }

        //
        // GET: /Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Locations/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        //
        // POST: /Locations/Edit/5
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
        // GET: /Locations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Locations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
