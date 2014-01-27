using CeMeOCore.DAL.Models;
using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.UnitsOfWork;
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
    public class MeetingsController : Controller
    {
        private MeetingControllerUoW _MeetingControllerUoW;

        public MeetingsController()
        {
            this._MeetingControllerUoW = new MeetingControllerUoW();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Title of the page
            ViewBag.Title = "Overview of all the Locations.";

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CreatorSortParm = String.IsNullOrEmpty(sortOrder) ? "Creator" : "";
            ViewBag.MeetingDateSortParm = sortOrder == "MeetingDate" ? "MeetingDate" : "MeetingDate";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "Location" : "Location";
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

            var mets = from s in this._MeetingControllerUoW.MeetingRepository.Get() select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                //mets = mets.Where(s => (s.Creator.LastName + " " + s.Creator.FirstName).Contains(searchString.ToUpper()));
                //TODO: Sorry Tycha!
                mets = null;
            }
            //End searching

            /*
            switch (sortOrder)
            {
                case "Name_desc":
                    mets = mets.OrderByDescending(s => s.Creator.LastName + " " + s.Creator.FirstName);
                    break;
                case "Street":
                    mets = mets.OrderBy(s => s.MeetingDate);
                    break;
                case "Number":
                    mets = mets.OrderByDescending(s => s.Location.Name);
                    break;
                case "City":
                    mets = mets.OrderByDescending(s => s.State);
                    break;
                default:
                    mets = mets.OrderByDescending(s => s.Creator.LastName + " " + s.Creator.FirstName);
                    break;
            }*/
            //TODO: Sorry Tycha!

            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(mets.ToPagedList(pageNumber, pageSize));
            //End sorting
        }

        //
        // GET: /Meetings/Details/5
        public ActionResult Details(int id)
        {
            var meetings = this._MeetingControllerUoW.MeetingRepository.dbSet.Find(id);
            return View(meetings);
        }

        //
        // POST: /Locations/Delete/5
        public void DeleteMeeting(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var original = this._MeetingControllerUoW.MeetingRepository.dbSet.Find(id);
                this._MeetingControllerUoW.MeetingRepository.dbSet.Remove(original);
                this._MeetingControllerUoW.MeetingRepository.context.SaveChanges();
                RedirectToAction("Index");
            }
            catch
            {
                RedirectToAction("Details");
            }
        }
    }
}
