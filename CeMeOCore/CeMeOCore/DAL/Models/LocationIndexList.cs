using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.DAL.Models
{
    public class LocationIndexList
    {
        public Room room { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; } // dropdown
        public String locationId { get; set; }
    }
}