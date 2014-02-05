using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models.Unused
{
    //Model for the Appointment table
    public class Appointment
    {
        [Key]
        public int appointmentID { get; set; }

        //Date at when the meeting is due
        [Required]
        [Display(Name = "Meetingdate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        //Time when the meeting starts
        [Required]
        [Display(Name = "Begintime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginTime { get; set; }

        //Time at when the meeting ends
        [Required]
        [Display(Name = "Endtime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndTime { get; set; }

        //Short description awhat the meeting is about, so users can see what kind of meeting it is.
        [Display(Name = "Description")]
        public String Description { get; set; }
        
        //Location where the meeting will be located
        [Required]
        [Display(Name = "Location")]
        public virtual Location Location {get; set;}

    }
}