using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [Display(Name = "LocationID")]
        public virtual Location LocationID { get; set; }

        [Required]
        [Display(Name = "Is there a beamer present?")]
        public bool BeamerPresent { get; set; }
    }
}