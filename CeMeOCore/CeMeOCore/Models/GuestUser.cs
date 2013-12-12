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
        [Required]
        public int GuestUserID { get; set; }

        [Required]
        public bool AccountAvailable { get; set; }

    }
}
