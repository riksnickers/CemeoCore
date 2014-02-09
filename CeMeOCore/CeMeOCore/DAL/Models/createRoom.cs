using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeMeOCore.DAL.Models
{
    public class CreateRoom
    {
        [Required(ErrorMessage = "A name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Minimum 1 characters required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Type is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Minimum 1 characters required")]
        public String Type{ get; set; }

        public IEnumerable<SelectListItem> ActionsList { get; set; }

        [Required(ErrorMessage = "A Location is required")]
        public String ActionId { get; set; }

        public List<TempRoom> locs {get; set; }
    }
}