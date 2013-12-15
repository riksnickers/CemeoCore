using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class Appointment
    {
        [Key]
        public int appointmentID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime beginTime { get; set; }

        [Required]
        public DateTime endTime { get; set; }

        public String Description { get; set; }
        
        [Required]
        public virtual Location location {get; set;}

    }
}