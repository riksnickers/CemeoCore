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
        
        private RoomUoW _roomUoW;

        public RoomController()
        {
            this._roomUoW = new RoomUoW();
        }

        // GET Room/Index
        /// <summary>
        /// Retruns a view with a tbale full of rooms
        /// </summary>
        /// <param name="sortOrder">Request message </param>
        /// <param name="currentFilter">currentsortfilter</param>
        /// <param name="searchString">searchstring taking care of the searching</param>
        /// <param name="page">paging parameter</param>
        /// <returns>vie with table of rooms</returns>
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

        // GET Room/Details/?
        /// <summary>
        /// get the details of a specific room
        /// </summary>
        /// <param name="id">specific room </param>
        /// <returns>view with details of a specific room</returns>
        public ActionResult Details(int id)
        {
            var meetingRoom = this._roomUoW.roomnRepository.dbSet.Find(id);
            return View(meetingRoom);
        }

        // GET Room/Create
        /// <summary>
        /// Retruns a view to create a new room
        /// </summary>
        /// <returns>view to create a new room</returns>
        public ActionResult Create()
        {
            CreateRoom temp = new CreateRoom();
            temp.ActionsList = (from a in _roomUoW.locationRepository.Get()
                                select new SelectListItem
                                {
                                    Text = a.Name,
                                    Value = a.LocationID.ToString()
                                }).ToList();

            temp.locs = new List<TempRoom>();
            foreach (Location element in _roomUoW.locationRepository.Get())
            {
                temp.locs.Add(new TempRoom() { ID = element.LocationID, Name = element.Name });
            }

            return View(temp);
        }

        // GET Room/Create
        /// <summary>
        /// create a new room
        /// </summary>
        /// <param name="Createroom"> data for new room </param>
        /// <returns>s</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRoom room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Room newRoomToAdd = new Room();
                    newRoomToAdd.Name = room.Name;
                    newRoomToAdd.Type = room.Type;
                    newRoomToAdd.LocationID = _roomUoW.locationRepository.dbSet.Find(Int32.Parse(room.ActionId));
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

        /// GET: /RoomTest/Edit/5
        /// <summary>
        /// Retruns a view to edit a sepcific room
        /// </summary>
        /// <param name="id"> specific room </param>
        /// <returns>vie with editable data for room</returns>
        public ActionResult Edit(int? id)
        {
            Room temp = this._roomUoW.roomnRepository.dbSet.Find(id);
            if (temp == null)
            {
                return HttpNotFound();
            }
            return View(temp);
 
        }

        /// POST: /RoomTest/Edit/5
        /// <summary>
        /// Retruns a view to edit a specific room
        /// </summary>
        /// <param name="temp"> room to edit </param>
        /// <returns>edit specific room</returns>
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

        /// POST: /Locations/Delete/?
        /// <summary>
        /// method to delete a room
        /// </summary>
        /// <param name="id">id of a specific room</param>
        /// <returns></returns>
        /// 
        public ActionResult Delete(int id)
        {
            try
            {
                var Location = this._roomUoW.roomnRepository.dbSet.Find(id);
                return View(Location);
            }
            catch
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, Location toDel)
        {
            try
            {
                var original = this._roomUoW.roomnRepository.dbSet.Find(id);
                this._roomUoW.roomnRepository.dbSet.Remove(original);
                this._roomUoW.roomnRepository.context.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
