using CeMeOCore.DAL.Repositories;
using CeMeOCore.Logic.Organiser.Exceptions;
using CeMeOCore.Logic.Range;
using CeMeOCore.Logic.Spots;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    public class Organiser : IOrganiser
    {
        /// <summary>
        /// An instance of the CeMeOContext
        /// </summary>
        private CeMeoContext _db = new CeMeoContext();

        /// <summary>
        /// The id of the OrganiserID process that will be used.
        /// </summary>
        public string OrganiserID { get; private set; }

        /// <summary>
        /// This is who requested to organise a meeting
        /// </summary>
        private UserProfile RequestedBy { get; set; }
        /// <summary>
        /// This is when the request was send to organise a meeting
        /// </summary>
        private DateTime DateRequested{ get; set;}

        /// <summary>
        /// All Participants for this process
        /// </summary>
        private Dictionary<string, Invitee> _invitees { get; set; }
        
        /// <summary>
        /// This is the deadline when the meeting must be planned
        /// </summary>
        public int DeadLineInDays { get; set; }


        private int TotalInvitees { get; set; }
        private int TotalInviteesAbsent { get; set; }
        private int TotalImporantInviteesAbsent { get; set; }
        private int TotalInviteesOnline { get; set; }
        private int TotalInviteesUnanswered { get; set; }

        private double Duration { get; set; }
        
        /// <summary>
        /// To start organising a meeting the constructor must be called
        /// </summary>
        /// <param name="participants">All of the invited attendees</param>
        /// <param name="dateRequested">When the organiser was started</param>
        /// <param name="deadLineInDays">This is the deadline</param>
        /// <param name="requestedById">Who requested to organise this meeting ID</param>
        public Organiser(IEnumerable<InvitedParticipant> participants, DateTime dateRequested, DateIndex deadLineInDays, int requestedById, double duration)
        {
            //Set counters to 0
            TotalImporantInviteesAbsent = 0;
            TotalInvitees = 0;
            TotalInviteesAbsent = 0;
            TotalInviteesOnline = 0;
            TotalInviteesUnanswered = 0;

            //Resolve requestedBy
            RequestedBy = resolveRequestedBy(requestedById);
            //Create unique ID
            OrganiserID = DateHash();

            //Set the duration
            this.Duration = duration;

            //Resolve invitedParticipants
            ConvertParticipantsToInvitees(participants);
           
            //CheckAttendeesCalendar/appointments
            CheckAvailabilityInvitees();

            //  CalculateEarliestAppointment - take blackspots in account
            CalculateEarliestProposition();

            sendPropositionToInvitees();
            //  If an attendee denies let it recalucate

            //If everyone accepted
            //  Plan an appointment in their calendar
            //  CreateMeeting
        }


        public Boolean ConvertParticipantsToInvitees(IEnumerable<InvitedParticipant> invitedParticipants)
        {
            try
            {
                foreach (var invitedParticipant in invitedParticipants)
                {
                    Invitee invitee = new Invitee(OrganiserID, invitedParticipant.id, invitedParticipant.Important);
                    this._invitees.Add(invitee.InviteeID, invitee);
                    TotalInviteesUnanswered++;
                    TotalInvitees++;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method will figure out all the blackspots.
        /// </summary>
        private void CheckAvailabilityInvitees()
        {
            //Generate blackspots until the deadline
        }

        private string DateHash()
        {
            StringBuilder returnVal = new StringBuilder();
            //GetTheCurrentDateTime
            String dateToHash = DateTime.Now.ToString();

            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(dateToHash + Guid.NewGuid().ToString());
            byte[] tempHash = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(tempSource);

            returnVal.Append(BitConverter.ToString(tempHash).Replace("-", "").ToLower());

            return returnVal.ToString();
        }

        private void ChangeReservedSpot(Guid ReservedSpotGuid, DateTime start, DateTime end)
        {
            ReservedSpot r = Startup.SpotManagerFactory().GetReservedSpot(ReservedSpotGuid);
            ReservedSpot rnew = new ReservedSpot(){DateRange = new DateRange(start, end), Guid = ReservedSpotGuid};
            Startup.SpotManagerFactory().ChangeReservedSpot(r.DateRange, rnew);
        }
        
        /// <summary>
        /// This method will calculate the earliest appointment possible.
        /// </summary>
        private void CalculateEarliestProposition()
        {
            //TODO: Add room logic
            //HACK: Refactor Person available logic.
            //This is our start proposal daterange
            ProposalDateRange proposalDateRange = null;
            Room proposalRoom = null;

            //A reserved spot will be added to the list to reserve this proposition.
            ReservedSpot reservedSpot = new ReservedSpot();
            reservedSpot.DateRange = proposalDateRange;
            Guid ReservedSpotGuid = reservedSpot.Guid;
            //TODO: change key reservedspot when daterange changes
            Startup.SpotManagerFactory().AddSpot(reservedSpot);

            //A new proposel to send to the invitees will be created
            Proposition proposition = new Proposition(ReservedSpotGuid);

            //Try to make a proposal
            try
            {
                proposalDateRange = GetProposalDate(proposition.ReservedSpotGuid);
                proposalRoom = GetProposalRoom(proposalDateRange, proposition.ReservedSpotGuid);

                //Now let's create the proposal.
                proposition.ProposedRoom = proposalRoom;
                proposition.BeginTime = proposalDateRange.Start;
                proposition.EndTime = proposalDateRange.End;

                foreach (Invitee inv in this._invitees.Values)
                {
                    inv.Proposal = proposition;
                }
            }
            catch (Exception)
            {
                //Something went wrong while find a room
                //TODO: Logging
            }          
        }

        private ProposalDateRange GetProposalDate(Guid reservedSpot)
        {
            ProposalDateRange proposalDateRange = new ProposalDateRange(DateTime.Now, DateTime.Now.AddSeconds(this.Duration));

            SortedList<DateRange, PersonBlackSpot> pbss = Startup.SpotManagerFactory().GetPersonBlackSpots(this.OrganiserID);
            //Try to find a good daterange looking at persons
            try
            {
                foreach (DateRange personBlackSpotDateRange in pbss.Keys)
                {
                    try
                    {
                        if (!personBlackSpotDateRange.Includes(proposalDateRange))
                        {
                            //Check if there are other persons who have a meeting at that time
                            //HACK: Refactor this linq so it has more performance
                            if (pbss.Keys.Select(dr => dr.Includes(proposalDateRange)).Count() != 0) //Count if a person has a daterange that conflicts with our date.
                            {
                                throw new PersonNotAvailableException();
                            }
                        }
                    }
                    catch (PersonNotAvailableException)
                    {
                        //Set the end time from the overlapping daterange as the start time for the proposalDateRange
                        proposalDateRange.ModifyStartDateTime(personBlackSpotDateRange.End, Duration);
                        ChangeReservedSpot(reservedSpot, proposalDateRange.Start, proposalDateRange.End);
                    }
                    catch (Exception)
                    {
                        //Throw it Up!
                        throw;
                    }
                }
            }
            catch (NoPersonsAvailableException)
            {

            }
            catch (Exception)
            {
                //Throw it up!
                throw;
            }
            return proposalDateRange;
        }

        private Room GetProposalRoom(ProposalDateRange proposalDateRange, Guid reservedSpot) 
        {
            SortedList<DateRange, RoomBlackSpot> rbss = Startup.SpotManagerFactory().GetRoomBlackSpots();
            SortedList<DateRange, ReservedSpot> rss = Startup.SpotManagerFactory().GetReservedSpots();

            Room proposalRoom = null;
            //TODO determine best location
            IEnumerable<Room> rooms = _db.Rooms;

            //Let's try to find a room
            try
            {
                //If it is 0 then set the proposal date is good. Wich will mean that everyone is available at that time.
                //Now let's check if their is a room available
                //Get a room and check if it's available.
                foreach (Room room in rooms)
                {
                    proposalRoom = room;
                    try
                    {
                        //enumerate all roomblackspots
                        foreach (DateRange roomBlackSpotDateRange in rbss.Keys)
                        {
                            //Check if the spot overlaps with the proposaldaterange
                            if (roomBlackSpotDateRange.Includes(proposalDateRange))
                            {
                                //We have found a overlap in the daterange, is it the room we are looking for?
                                RoomBlackSpot rs = rbss[roomBlackSpotDateRange];
                                if (rs.Room.Equals(proposalRoom))
                                {
                                    throw new RoomNotAvailableException();
                                }
                            }
                        }

                        //Now check if the proposalroom is reserved at the moment
                        //First check if a daterange overlaps with the proposal daterange
                        foreach (DateRange reservedSpotDateRange in rss.Keys)
                        {
                            if (reservedSpotDateRange.Includes(proposalDateRange))
                            {
                                //We have found a overlap in the daterange, is it the room we are looking for?
                                ReservedSpot rs = rss[reservedSpotDateRange];
                                //Does the reservedSpot includes the room And it's not our reserved spot.
                                if (rs.Includes(proposalRoom) && (rs.Guid != reservedSpot))
                                {
                                    throw new RoomIsReservedException();
                                }
                            }
                        }
                    }
                    catch (RoomNotAvailableException)
                    {
                        //It's the room we are looking for; but it's not available 
                        //We have a NO GO for this room!
                        //Let's continue looking for a room
                        proposalRoom = null;
                    }
                    catch (RoomIsReservedException)
                    {
                        //The room is reserved for an other meeting at the same time.
                        //We have a NO GO for this room! but this room might be available later
                        //TODO: add listener when rooms becomes available..?
                        proposalRoom = null;
                    }
                    catch (Exception)
                    {
                        //Throw it up!
                        throw;
                    }

                    //End of room foreach
                }
                //When all the rooms are iterated check if we found a room.
                if (proposalRoom == null)
                {
                    throw new NoRoomsFoundException();
                }
            }
            catch (NoRoomsFoundException)
            {
                //Arg we found no rooms for this proposal.. Let the creator know of this.
                //TODO: Throw error or change dateRange (or shorten the duration?)

                //Throw it up! But log it first ;)
                throw;
            }
            catch (Exception)
            {
                //Throw it up!
                throw;
            }
            return proposalRoom;
        }

        /// <summary>
        /// This method will send a proposition to all invitees. The Inviter class will handle all these request/respsonses
        /// </summary>
        private void sendPropositionToInvitees()
        {
            //This will need to send the proposition to the right invitee
            //Push notification
        }

        /// <summary>
        /// This will resolve the id of the creator
        /// </summary>
        /// <param name="userProfileId">The id of who requested the meeting
        /// </param>
        /// <returns></returns>
        private UserProfile resolveRequestedBy( int userProfileId)
        {
            try
            {
                UserProfileRepository userRep = new UserProfileRepository(_db);
                return userRep.GetByID(userProfileId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean IsTheMeetingReadyToGo()
        {
            /*
            if(this.LocalInviter.DidAllInviteesAccept())
            {
                return true;
            }*/
            return false;
        }

        public Boolean registerAvailabilityInvitee(PropositionAnswerBindingModel model)
        {
            if (this._invitees.ContainsKey(model.InviteeID))
            {
                this._invitees[model.InviteeID].Answer = model.Answer;
                CheckAnswer(model.InviteeID);
            }

            //TODO:Should this be a new feature? HowManyCantCome()
            //Notify the Creator if to many non-imporant people can't come.
            //Let's say that if X% of the people did not accept it will send a notice to the creator if the meeting should continue
            //Or even another deadline

            return false;
        }

        private Boolean CheckAnswer(string id)
        {
            //TODO: If their is send a new proposition reduce Totals!
            if (this._invitees[id].Important == true && this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Reschudele meeting
                //Let the System know that it need to reschedule everything
                //But add a blackspot to the existing list

                TotalImporantInviteesAbsent++;
                TotalInviteesAbsent++;

                return false;
            }
            else if (this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Let the system decide if their is a new proposition needed.
                //If their is no new proposition the meeting will continue (unless he joins online)

                TotalInviteesAbsent++;

                return false;
            }
            else if ( this._invitees[id].Answer == Availability.Online )
            {
                //TODO:Proposition may not be online at the first try
                //Example: Could you be present at this meeting Present/Absent
                //When the person chose Absent they get another message 
                //from the system asking if they want to be present Online
                //Increase the OnlineCounter for this organisation. 
                TotalInviteesOnline++;
            }
            TotalInviteesUnanswered--;
            return true;
        }

        public OrganiserResponse GetStatus()
        {
            //TODO: Get the status of the organiser
            throw new NotImplementedException();
        }

        public Proposition GetProposition(string inviteeID)
        {
            throw new NotImplementedException();
        }
    }

    public class InvitedParticipant
    {
        [Required]
        public int id { get; set; }

        [Required]
        public Boolean Important { get; set; }
    }
}