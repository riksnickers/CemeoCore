using CeMeOCore.DAL.Models;
using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CeMeOCore.Controllers
{
    [Authorize(Users = "Admin")]
    public class UserProfileController : Controller
    {
        private UserUoW _UserUoW;

        public UserProfileController()
        {
            this._UserUoW = new UserUoW();
        }

        [Authorize(Users = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Title of the page
            ViewBag.Title = "Overview of all the Locations.";

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName" : "LastName";
            ViewBag.EMailPresentSortParm = sortOrder == "EMail" ? "EMail" : "EMail";
            ViewBag.PreferedLocationSortParm = sortOrder == "PreferedLocation" ? "PreferedLocation" : "PreferedLocation";
            ViewBag.UserCalendarSortParm = sortOrder == "UserCalendar" ? "UserCalendar" : "UserCalendar";

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

            var users = from s in this._UserUoW.UserProfileRepository.Get() select s;

            //Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.Contains(searchString));
            }
            //End searching

            switch (sortOrder)
            {
                case "UserName":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "FirstName":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    users = users.OrderByDescending(s => s.LastName);
                    break;
                case "EMail":
                    users = users.OrderByDescending(s => s.EMail);
                    break;
                case "Preferedlocation":
                    users = users.OrderByDescending(s => s.PreferedLocation);
                    break;
                case "UserCalendar":
                    users = users.OrderByDescending(s => s.UserCalendar);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
            //End sorting
        }
    }
}
