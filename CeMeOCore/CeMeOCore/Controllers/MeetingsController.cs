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
    public class MeetingsController : Controller
    {
        //Still need to delete the _db things...
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /Meetings/
        /*blic ActionResult Index()
        {
            ViewBag.Title = "Overview of all the meetings.";
            var model = _db.Meetings;
            return View(model);
        }*/
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

            var mets = from s in _db.Meetings select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                mets = mets.Where(s => (s.Creator.LastName + " " + s.Creator.FirstName).Contains(searchString.ToUpper()));
            }
            //End searching

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
            }

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
            var meetings = _db.Meetings.Find(id);
            return View(meetings);
        }

        // GET: /MeetingControllerTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = _db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: /MeetingControllerTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = _db.Meetings.Find(id);
            _db.Meetings.Remove(meeting);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
