using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class RoomController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        List<Location> locations = new List<Location>();
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the Meeting rooms.";
            var model = _db.Rooms;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var meetingRoom = _db.Rooms.Find(id);
            return View(meetingRoom);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                room.LocationID.LocationID = 0;
                //locations = _db.Locations.ToList();
                _db.Rooms.Add(room);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(room);
        }

        // GET: /RoomTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room temp = _db.Rooms.Find(id);
            if (temp == null)
            {
                return HttpNotFound();
            }
            return View(temp);
 
        }

        // POST: /RoomTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room temp)
        {
            Room room = _db.Rooms.Find(temp.RoomID);
            
            if (ModelState.IsValid)
            {
                
                room.Name = temp.Name;
                room.Type = temp.Type;
                room.LocationID = temp.LocationID;

                if (temp.BeamerPresent)
                {
                    room.BeamerPresent = true;
                }
                else
                {
                    room.BeamerPresent = false;
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(room);

            
        }

        // GET: /RoomTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: /RoomTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = _db.Rooms.Find(id);
            _db.Rooms.Remove(room);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
