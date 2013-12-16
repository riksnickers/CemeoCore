using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CeMeOCore.Models
{
    public class GuestUser
    {
        [Key]
        [Display(Name = "GuestID")]
        public int GuestUserID { get; set; }

        [Required]
        [Display(Name = "Is the account available?")]
        public bool AccountAvailable { get; set; }

    }
}
