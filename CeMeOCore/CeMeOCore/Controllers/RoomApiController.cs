using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class RoomApiController : ApiController
    {
        private CeMeoContext _db = new CeMeoContext();
        static readonly Room repository = new Room();

        private ActionResult View()
        {
            throw new NotImplementedException();
        }

        // GET api/rooms
        // Get all meetingRooms that are located in the database
        public IEnumerable<Room> GetAll()
        {
            return _db.Rooms;
        }

        // GET api/room/5
        public Room Get(int id)
        {
            var meetingRoom = _db.Rooms.FirstOrDefault((p) => p.RoomID == id);
            if (meetingRoom == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return meetingRoom;
        }

        // POST api/room
        public void Add([FromBody]Room meetingRoom)
        {
            if (meetingRoom == null)
            {
                throw new ArgumentNullException("item");
            }
            _db.Rooms.Add(meetingRoom);
            _db.SaveChanges();
        }

        // PUT api/room/5
        public void Update([FromBody]Room meetingRoom)
        {
            _db.Entry(meetingRoom).State = EntityState.Modified;
            _db.SaveChanges();
        }

        // DELETE api/room/5
        public void Delete([FromBody]int id)
        {
            Room meetingRoom = _db.Rooms.Find(id);
            _db.Rooms.Remove(meetingRoom);
            _db.SaveChanges();
        }
    }
}
