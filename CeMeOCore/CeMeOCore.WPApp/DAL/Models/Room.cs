using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeO.WPApp.DAL.Models
{
    class Room
    {
        public int RoomID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual Location LocationID { get; set; }

        public bool BeamerPresent { get; set; }
    }
}
