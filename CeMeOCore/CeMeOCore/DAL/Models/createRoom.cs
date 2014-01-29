using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.DAL.Models
{
    public class CreateRoom
    {
        public Room room { get; set; }
        public IEnumerable<SelectListItem> ActionsList { get; set; }  
        public String ActionId { get; set; }

        public CreateRoom()
        {
            ActionId = "";
            ActionsList = new List<SelectListItem>();
        }
    }
}