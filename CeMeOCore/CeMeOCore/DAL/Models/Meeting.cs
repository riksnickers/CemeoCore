using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.Models
{
    public class Meeting
    {
        [Key]
        [Display(Name = "MeetingID")]
        public int MeetingID { get; set; }

        [Required]
        [Display(Name = "Creator")]
        public UserProfile Creator { get; set; }

        [Required]
        [Display(Name = "MeetingDate")]
        public DateTime MeetingDate { get; set; }

        [Required]
        [Display(Name = "Location")]
        public Location Location { get; set; }

        [Required]
        [Display(Name = "Current state")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number as state")]
        public int State { get; set; }

        public virtual ICollection<MeetingUser> MeetingUser { get; set; }
        
        public Meeting()
        {
            MeetingUser = new List<MeetingUser>();
        }
    }

    public class ScheduleMeetingBindingModel
    {
        [Required]
        [Display(Name = "Creator")]
        public int Creator { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public double Duration { get; set; }

        [Required]
        [Display(Name = "Invited Participants")]
        public List<InvitedParticipant> InvitedParticipants { get; set; }

        [Required]
        [Display(Name = "Date Index")]
        public DateIndex Dateindex { get; set; }
        [Display(Name = "Before date")]
        public DateTime BeforeDate { get; set; }
    }

    public class CancelMeetingBindingModel
    {
        [Required]
        [Display(Name = "MeetingID")]
        public int MeetingID { get; set; }

        [Required]
        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [Required]
        [Display(Name = "CanceledBy")]
        public int CanceledBy { get; set; }
    }

    public class ChangeDeadlineMeetingBindingModel
    {
        [Required]
        [Display(Name = "MeetingID")]
        public int MeetingID { get; set; }

        [Required]
        [Display(Name = "New Deadline Days")]
        public int NewDeadLineDays  { get; set; }
    }
}