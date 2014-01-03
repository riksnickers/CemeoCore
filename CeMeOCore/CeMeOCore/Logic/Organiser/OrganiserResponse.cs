using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    public class OrganiserResponse
    {
        public string OrganiserResponseID { get; set; }

        public string ResponseMessage { get; set; }

        public string ResponseExtraInfo { get; set; }
       
        public static Dictionary<string, OrganiserResponse> Response = new Dictionary<string, OrganiserResponse>();

        private static List<OrganiserResponse> Responses = new List<OrganiserResponse>()
        {
            //AllAttendeesAccepted
            {
                new OrganiserResponse() 
                {
                    OrganiserResponseID = "0x001",
                    ResponseMessage = "All attendees accepted the invitation."
                }
            },

            //AllImportantAttendeesAccepted
            {
                new OrganiserResponse() 
                {
                    OrganiserResponseID = "0x002",
                    ResponseMessage = "All important attendees accepted the invitation."
                }
            },

            //AllImportantAttendeesAccpetedSomeCancelled
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x003",
                    ResponseMessage = "All important attendees accepted the invitation, but some of the non important persons cancelled"
                }
            },

            //AnImportAttendeeCancelled
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x004",
                    ResponseMessage = "An important attendee did not accept the proposition"
                }
            },

            //SuitableLocationFound
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x005",
                    ResponseMessage = "Suitable location found for this upcomming meeting."
                }
            },

            //NoSuitableLocationFound
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x006",
                    ResponseMessage = "No suitable location found for this upcomming meeting. Try another deadline."
                }
            },

            //MeetingSuccessfullyOrganised
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x007",
                    ResponseMessage = "The meeting was successfully Organised"
                }
            },

            //MeetingCancelled
            {
                new OrganiserResponse()
                {
                    OrganiserResponseID = "0x008",
                    ResponseMessage = "The meeting got cancelled"
                }
            }
        };

        static OrganiserResponse()
        {
            foreach (OrganiserResponse item in Responses)
            {
                Response.Add(item.OrganiserResponseID, item);
            }
            Responses.Clear();
        }
    }
}