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

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 characters required")]
        [Required(ErrorMessage = "A name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid")]
        public string Name { get; set; }

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 1 characters required")]
        [Required(ErrorMessage = "A locationname is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "LocationID")]
        public virtual Location LocationID { get; set; }
    }
}