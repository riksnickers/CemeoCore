using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CeMeOCore.Models
{
    //This is a sor of an useraccount that can be enabled temporary for external people, so they can use the app too.
    public class GuestUser
    {
        [Key]
        [Display(Name = "GuestID")]
        public int GuestUserID { get; set; }

        //Is the guest account available?
        [Required]
        [Display(Name = "Is the account available?")]
        public bool AccountAvailable { get; set; }

    }
}
