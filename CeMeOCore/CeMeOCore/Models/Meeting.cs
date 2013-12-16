using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class Meeting
    {
        [Key]
        [Display(Name = "MeetingID")]
        public int MeetingID { get; set; }

        [Required]
        [Display(Name = "Creator")]
        public UserProfile Creator { get; set; }

        [Required]
        [Display(Name = "MeetingDate")]
        public DateTime MeetingDate { get; set; }

        [Required]
        [Display(Name = "Location")]
        public Location Location { get; set; }

        [Required]
        [Display(Name = "Current state")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number as state")]
        public int State { get; set; }
    }
}