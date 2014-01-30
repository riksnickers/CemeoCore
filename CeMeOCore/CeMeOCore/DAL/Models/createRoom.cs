using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.DAL.Models
{
    public class CreateRoom
    {
        public String Name { get; set; }
        public String Type{ get; set; }
        public IEnumerable<SelectListItem> ActionsList { get; set; }  
        public String ActionId { get; set; }

    }
}