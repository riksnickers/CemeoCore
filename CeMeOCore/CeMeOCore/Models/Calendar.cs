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
        public int CalendarID { get; set; }

        [Required]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}