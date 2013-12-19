using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        //
        // GET: /Meetings/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Meetings/Create
        [HttpPost]
        public ActionResult Create(Meeting met)
        {
            try
            {
                if (met == null)
                {
                    throw new ArgumentNullException("item");
                }
                _db.Meetings.Add(met);
                _db.SaveChanges();
                return View(met);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Meetings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Meetings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Meeting met)
        {
            try
            {
                _db.Entry(met).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Meetings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Meetings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Meeting met)
        {
            try
            {
                _db.Meetings.Remove(met);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
