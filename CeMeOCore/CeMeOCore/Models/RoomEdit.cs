using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class RoomEdit
    {
        public int RoomID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public virtual Location LocationID { get; set; }
        public bool BeamerPresent { get; set; }
    }
}