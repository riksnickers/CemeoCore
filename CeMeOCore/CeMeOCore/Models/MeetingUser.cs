﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class MeetingUser
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public UserProfile UserID { get; set; }

        [Key]
        [Required]
        public Meeting MeetingID { get; set; }

        [Required]
        public int Confirmed { get; set; }
        //Confirmation options:
        //0: unconfirmed
        //1: confirmed
        //2: declined

        [Required]
        public int Presence { get; set; }
        //Presence options
        //0: Present on site
        //1: Present online
        //2: Not present

        [Required]
        [StringLength(100)]
        public string Room { get; set; }
    }
}