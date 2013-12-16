using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class MeetingUser
    {
        public enum Confirmation 
        {
            Unconfirmed = 0,
            Confirmed = 1,
            Declined = 2
        }

        public enum Presences
        {
            Unconfirmed = 0,
            Confirmed = 1,
            Declined = 2
        }

        [Key, Column(Order = 1)]
        public virtual Meeting MeetingID { get; set; }


        [Key, Column(Order = 0)]
        public virtual UserProfile UserID { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "Please enter valid presence option; 0 = Unconfirmed, 1 = Confirmed, 2 = Declined")]
        public Confirmation Confirmed{ get; set; }
        //Confirmation options:
        //0: unconfirmed
        //1: confirmed
        //2: declined

        [Required]
        [Range(0, 2, ErrorMessage = "Please enter valid presence option; 0 = Present on site, 1 = Present online, 2 = Not present")]
        public Presences Presence { get; set; }
        //Presence options
        //0: Present on site
        //1: Present online
        //2: Not present

        [Required]
        [StringLength(100)]
        public string Room { get; set; }
    }
}