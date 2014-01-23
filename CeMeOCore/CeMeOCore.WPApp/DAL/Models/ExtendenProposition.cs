using CeMeO.WPApp.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeO.WPApp.DAL.Models
{
    class ExtendenProposition
    {
        public ExtendenProposition()
        {
            Others = new List<UserProfileCompact>();
        }
        public string InviteeID { get; set; }
        public Availability Answer { get; set; }
        public Proposition Proposition { get; set; }

        public List<UserProfileCompact> Others { get; set; }
    }
}
