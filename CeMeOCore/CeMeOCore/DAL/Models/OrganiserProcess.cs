using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class OrganiserProcess
    {
        public OrganiserProcess()
        {
            //EF
        }

        public OrganiserProcess( string organiserID, int requestedBy )
        {
            id = Guid.NewGuid().ToString();
            OrganiserID = organiserID;
            Status = OrganiserStatus.Initializing;
            //Set counters to 0
            TotalImporantInviteesAbsent = 0;
            TotalInvitees = 0;
            TotalInviteesAbsent = 0;
            TotalInviteesOnline = 0;
            TotalInviteesUnanswered = 0;
            Duration = 30;
            DateRequested = DateTime.Now;
            RequestedByID = requestedBy;
        }

        [Key]
        public string id { get; set; }
        
        public string OrganiserID { get; set; }

        /// <summary>
        /// This is the deadline when the meeting must be planned
        /// </summary>
        public int DeadLineInDays { get; set; }

        public OrganiserStatus Status { get; set; }

        public int TotalInvitees { get; set; }
        public int TotalInviteesAbsent { get; set; }
        public int TotalImporantInviteesAbsent { get; set; }
        public int TotalInviteesOnline { get; set; }
        public int TotalInviteesUnanswered { get; set; }

        public double Duration { get; set; }

        /// <summary>
        /// This is who requested to organise a meeting
        /// </summary>
        public int RequestedByID { get; set; }
        /// <summary>
        /// This is when the request was send to organise a meeting
        /// </summary>
        public DateTime DateRequested { get; set; }
    }
}