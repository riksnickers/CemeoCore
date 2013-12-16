using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CeMeOCore.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [StringLength(100)]
        public String Street { get; set; }

        [Required]
        public String Number { get; set; }

        [Display(Name = "Optional addition")]
        public int Addition { get; set; }

        [Required]
        public String Zip { get; set; }

        [Required]
        [StringLength(100)]
        public String City { get; set; }

        [StringLength(100)]
        public String State { get; set; }

        [Required]
        [StringLength(100)]
        public String Country { get; set; }
    }
}
