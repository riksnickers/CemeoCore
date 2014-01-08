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
        

        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the meeting rooms.";
            var model = _db.Rooms;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var meetingRoom = _db.Rooms.FirstOrDefault((p) => p.RoomID == id);
            return View(meetingRoom);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Room toAdd)
        {
            var insert = new Room();
            insert.Name = toAdd.Name;
            insert.Type = toAdd.Type;
            insert.LocationID = _db.Locations.Find(toAdd.LocationID);
            insert.BeamerPresent = false;

            _db.Rooms.Add(insert);
            _db.SaveChanges();

            return View("List");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Room meetingRoom)
        {
            _db.Entry(meetingRoom).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int roomid)
        {
            var rooms = _db.Rooms.Find(roomid);

            if (!User.IsInRole("Administrattion"))
            {
                return RedirectToAction("List");
            }

            return View(rooms);
        }

        [HttpPost]
        public ActionResult Delete(int roomid, Room rooms)
        {
            var original = _db.Rooms.Find(roomid);
            if (!User.IsInRole("Roomd" + original.RoomID))
            {
                return RedirectToAction("List");
            }

            int project = original.RoomID;

            _db.Rooms.Remove(original);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = rooms });
        }
    }
}
