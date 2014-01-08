using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class EditLocation
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Street { get; set; }

        [Required]
        public String Number { get; set; }

        [Required]
        public String Zip { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        public String Country { get; set; }

        public String State { get; set; }

        public int Addition { get; set; }
    }
}