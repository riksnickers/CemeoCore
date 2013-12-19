using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class UserProfileController : Controller
    {
        private CeMeoContext _db = new CeMeoContext();
        //
        // GET: /UserProfile/
        public ActionResult Index()
        {
            ViewBag.Title = "Overview of all the user.";
            var model = _db.Users;
            return View(model);
        }

        //
        // GET: /UserProfile/Details/5
        public ActionResult Details(int id)
        {
            var usr = _db.Users.Find(id);
            return View(usr);
        }

        //
        // GET: /UserProfile/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserProfile/Create
        [HttpPost]
        public ActionResult Create(UserProfile usr)
        {
            try
            {
                if (usr == null)
                {
                    throw new ArgumentNullException("item");
                }
                _db.Users.Add(usr);
                _db.SaveChanges();
                return View(usr);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserProfile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UserProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserProfile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserProfile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserProfile usr)
        {
            try
            {
                _db.Users.Remove(usr);
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
