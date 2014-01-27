using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CeMeOCore.DAL.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        //Name of a location
        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        //Streetname of where the location is situated
        [Required]
        [StringLength(100)]
        public String Street { get; set; }

        //Number on the street where the location is situated
        [Required]
        public String Number { get; set; }

        //postal code of where the location is situated
        [Required]
        public String Zip { get; set; }

        //City of the location
        [Required]
        [StringLength(100)]
        public String City { get; set; }

        //State of the location, this not required because some countries don't have states
        [StringLength(100)]
        public String State { get; set; }

        //country of where the location is situated
        [Required]
        [StringLength(100)]
        public String Country { get; set; }

        //an addition is not required
        public int Addition { get; set; }
    }

    public class SetLocationBindingModel
    {
        [Required]
        public int LocationID { get; set; }
    }
}
