using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string aspUser { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string EMail { get; set; }

        public virtual Location PreferedLocation { get; set; }

        [Required]
        public virtual Calendar UserCalendar { get; set; }

        //public virtual ICollection<MeetingUser> MeetingUser { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
        
        public UserProfile()
        {
            //MeetingUser = new List<MeetingUser>();
            Attendees = new HashSet<Attendee>();
        }
    }

    public class UserProfileBindingModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string aspUser { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string EMail { get; set; }

        public Location PreferedLocation { get; set; }
    }

    public class UserProfileCompact
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}