using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using PagedList;


namespace CeMeOCore.Controllers
{
    public class RoomController : Controller
    {
        private LocationIndexList locations = new LocationIndexList();
        private RoomUoW _roomUoW;

        public RoomController()
        {
            this._roomUoW = new RoomUoW();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Title of the page
            ViewBag.Title = "Overview of all the Locations.";

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "Type" : "Type";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "Location" : "Location";

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

            var rooms = from s in this._roomUoW.roomnRepository.Get() select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(s => s.Name.Contains(searchString));
            }
            //End searching

            switch (sortOrder)
            {
                case "Name":
                    rooms = rooms.OrderByDescending(s => s.Name);
                    break;
                case "Type":
                    rooms = rooms.OrderBy(s => s.Type);
                    break;
                case "Location":
                    rooms = rooms.OrderByDescending(s => s.LocationID.Name);
                    break;
                default:
                    rooms = rooms.OrderBy(s => s.Name);
                    break;
            }


            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(rooms.ToPagedList(pageNumber, pageSize));
            //End sorting
        }

        public ActionResult Details(int id)
        {
            var meetingRoom = this._roomUoW.roomnRepository.dbSet.Find(id);
            return View(meetingRoom);
        }

        public ActionResult Create()
        {
            locations.LocationList = (from u in this._roomUoW.locationRepository.Get().AsEnumerable()
                                      select new SelectListItem
                                      {
                                          Text = u.Name,
                                          Value = u.LocationID.ToString()
                                      }).AsEnumerable();
            locations.room = new Room();
            return View(locations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationIndexList room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.ViewBag.locationName = new SelectList(this._roomUoW.locationRepository.context.Locations, "locationName", "Name", room.room.LocationID.Name);
                    Room newRoomToAdd = new Room();
                    newRoomToAdd.Name = room.room.Name;
                    newRoomToAdd.Type = room.room.Type;
                    newRoomToAdd.LocationID = room.room.LocationID;
                    this._roomUoW.roomnRepository.dbSet.Add(newRoomToAdd);
                    this._roomUoW.roomnRepository.context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: /RoomTest/Edit/5
        public ActionResult Edit(int? id)
        {
            Room temp = this._roomUoW.roomnRepository.dbSet.Find(id);
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
            try
            {
                Room room = this._roomUoW.roomnRepository.dbSet.Find(temp.RoomID);
            
                if (ModelState.IsValid)
                {   
                    room.Name = temp.Name;
                    room.Type = temp.Type;
                    room.LocationID.Name = temp.LocationID.Name;
                    this._roomUoW.roomnRepository.context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Locations/Delete/5
        public void DeleteRoom(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var original = this._roomUoW.roomnRepository.dbSet.Find(id);
                this._roomUoW.roomnRepository.dbSet.Remove(original);
                this._roomUoW.roomnRepository.context.SaveChanges();
                RedirectToAction("Index");
            }
            catch
            {
                RedirectToAction("Details");
            }
        }

    }
}
