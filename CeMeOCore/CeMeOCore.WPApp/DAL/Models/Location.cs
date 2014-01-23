using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeO.WPApp.DAL.Models
{
    class Location
    {
        public int LocationID { get; set; }

        public String Name { get; set; }

        public String Street { get; set; }

        public String Number { get; set; }

        public String Zip { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Country { get; set; }

        public int Addition { get; set; }
    }
}
