using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeO.WPApp.DAL.Models
{
    class Proposition
    {
        public Proposition() { }
        
        public virtual Room ProposedRoom { get; set; }

        //Time when the meeting starts
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
