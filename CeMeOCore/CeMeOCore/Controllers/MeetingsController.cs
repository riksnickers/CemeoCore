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

        // GET: /MeetingControllerTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MeetingControllerTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingID,MeetingDate,Location,State")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _db.Meetings.Add(meeting);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meeting);
        }

        // GET: /MeetingControllerTest/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /MeetingControllerTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingID,MeetingDate,State")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(meeting).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
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
