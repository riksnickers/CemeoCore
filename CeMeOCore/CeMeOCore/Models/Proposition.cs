using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeMeOCore.Models
{
    public class Proposition
    {
        [Key]
        public string Id { get; set; }
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

    //This is the bindingModel for when a user request their proposition.
    public class GetPropositionBindingModel
    {
        [Required]
        public string OrganiserID { get; set; }
        [Required]
        public string InviteeID { get; set; }
    }


    /// <summary>
    /// This is the BindingModel for when an proposition answer is posted to the api.
    /// </summary>
    public class PropositionAnswerBindingModel
    {
        [Required]
        public string OrganiserID { get; set; }
        [Required]
        public string InviteeID { get; set; }
        [Required]
        public Availability Answer { get; set; }
    }
}