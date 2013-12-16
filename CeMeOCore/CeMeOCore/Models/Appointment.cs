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
        [Display(Name = "Meetingdate")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Begintime")]
        public DateTime BeginTime { get; set; }

        [Required]
        [Display(Name = "Endtime")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }
        
        [Required]
        [Display(Name = "Location")]
        public virtual Location Location {get; set;}

    }
}