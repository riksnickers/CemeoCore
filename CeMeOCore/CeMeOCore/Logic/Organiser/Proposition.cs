using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    public class Proposition
    {
        public Guid ReservedSpotGuid { get; private set; }
        // A proposition is for an invitee wich contains a Room (+location) and a start span
        public Proposition( Guid reservedSpotGuid)
        {
            this.ReservedSpotGuid = reservedSpotGuid;
        }
        
        public Room ProposedRoom { get; set; }

        //Time when the meeting starts
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginTime { get; set; }

        //Time at when the meeting ends
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndTime { get; set; }
    }
}