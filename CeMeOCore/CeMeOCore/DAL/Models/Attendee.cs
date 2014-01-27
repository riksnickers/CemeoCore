using Newtonsoft.Json;
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
        public virtual Room Room { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserProfile> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<Meeting> Meetings { get; set; }
    }

    public class MeetingInformation
    {
        public Meeting Meeting { get; set; }
        public Attendee Self { get; set; }
        public HashSet<UserProfileCompact> Others { get; set; }
        public MeetingInformation()
        {
            Others = new HashSet<UserProfileCompact>();
        }
    }
}