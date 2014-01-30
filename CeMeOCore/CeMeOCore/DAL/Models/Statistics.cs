using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class Statistics
    {
        public int countMeetings { get; set; }
        public int countLocations { get; set; }
        public int countUsers { get; set; }
        public int countRooms { get; set; }
        public int countforenoons { get; set; }
        public int countafternoons { get; set; }
    }
}