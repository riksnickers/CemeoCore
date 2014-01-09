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
    public class MeetingsController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /Meetings/
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the meetings.";
            var model = _db.Meetings;
            return View(model);
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
