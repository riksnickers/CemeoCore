using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class Calendar
    {
        [Key]
        [Display(Name = "CalendarID")]
        public int CalendarID { get; set; }

        [Required]
        [Display(Name = "Meeting appointments")]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}