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
        [Key]
        public string InviteeID { get; set; }

        public int UserID { get; set; }

        private string OrganiserID { get; set; }

        public Boolean Important { get; set; }

        public Availability Answer { get; set; }

        public Proposition Proposal { get; set; }

        public Invitee(string organiserID, int userID, Boolean important)
        {
            InviteeID = DateHash(organiserID) + "#" + userID;
            UserID = userID;
            OrganiserID = organiserID;
            Important = important;
            Proposal = null;
            Answer = Availability.Unanswered;
        }

        private string DateHash(string organiserID)
        {
            StringBuilder returnVal = new StringBuilder();
            //GetTheCurrentDateTime
            //HACK: Change hashing with random int
            String dateToHash = DateTime.Now.ToString();
            Random r = new Random();
            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(dateToHash + r.Next(200000).ToString() + organiserID);
            byte[] tempHash = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(tempSource);

            returnVal.Append(BitConverter.ToString(tempHash).Replace("-", "").ToLower());

            return returnVal.ToString();
        }

        public Proposition GetProposition()
        {
            return this.Proposal;
        }
    }

    public class InviteeInformationModel
    {
        public string InviteeID { get; set; }
    }
}