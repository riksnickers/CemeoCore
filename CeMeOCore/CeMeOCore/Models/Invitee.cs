using CeMeOCore.Logic.Organiser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace CeMeOCore.Models
{
    public class Invitee
    {
        public string InviteeID { get; set; }

        public string UserID { get; set; }

        public Boolean Important { get; set; }

        public Availability Answer { get; set; }

        public Proposition Proposal { get; set; }

        public Invitee(string organiserID, string userID, Boolean important)
        {
            InviteeID = dateHash(organiserID) + "#" + userID;
            UserID = userID;
            Important = important;
            Answer = Availability.Unanswered;
        }

        private string dateHash(string organiserID)
        {
            StringBuilder returnVal = new StringBuilder();
            //GetTheCurrentDateTime
            String dateToHash = DateTime.Now.ToString();

            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(dateToHash + organiserID);
            byte[] tempHash = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(tempSource);

            returnVal.Append(BitConverter.ToString(tempHash).Replace("-", "").ToLower());

            return returnVal.ToString();
        }
    }

    /// <summary>
    /// This is the BindingModel for when an answer is posted to the api.
    /// </summary>
    public class InviterAnswerBindingModel
    {
        [Required]
        public string OrganiserID { get; set; }
        [Required]
        public string InviteeID { get; set; }
        [Required]
        public Availability Answer { get; set; }
    }
}