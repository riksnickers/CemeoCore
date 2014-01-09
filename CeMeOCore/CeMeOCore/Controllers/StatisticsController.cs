using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Statistics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
