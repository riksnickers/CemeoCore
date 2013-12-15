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
        public int MeetingID { get; set; }

        [Required]
        public UserProfile Creator { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public int State { get; set; }
    }
}