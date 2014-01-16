﻿using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName" : "LastName";
            ViewBag.EMailPresentSortParm = sortOrder == "EMail" ? "EMail" : "EMail";
            ViewBag.PreferedLocationSortParm = sortOrder == "PreferedLocation" ? "PreferedLocation" : "PreferedLocation";
            ViewBag.UserCalendarSortParm = sortOrder == "UserCalendar" ? "UserCalendar" : "UserCalendar";

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

            var users = from s in _db.Users select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.Contains(searchString));
            }
            //End searching

            switch (sortOrder)
            {
                case "UserName":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "FirstName":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    users = users.OrderByDescending(s => s.LastName);
                    break;
                case "EMail":
                    users = users.OrderByDescending(s => s.EMail);
                    break;
                case "Preferedlocation":
                    users = users.OrderByDescending(s => s.PreferedLocation);
                    break;
                case "UserCalendar":
                    users = users.OrderByDescending(s => s.UserCalendar);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
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
