using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class Attendee
    {
        [Key]
        public virtual int MeetingId { get; set; }

        [Key]
        public virtual int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public Room Room { get; set; }

        public virtual ICollection<UserProfile> users { get; set; }
        public virtual ICollection<Meeting> meetings { get; set; }
    }
}