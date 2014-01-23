using CeMeOCore.DAL.Repositories;
using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.Logic.Organiser.Exceptions;
using CeMeOCore.Logic.Range;
using CeMeOCore.Logic.Spots;
using CeMeOCore.DAL.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace CeMeOCore.Logic.Organiser
{
    /// <summary>
    /// The organiser will organise a meeting
    /// </summary>
    public class Organiser : IOrganiser
    {
        public event EventHandler DeadLineChanged;

        /// <summary>
        /// The id of the OrganiserID process that will be used.
        /// </summary>
        public string OrganiserID { get; private set; }



        /// <summary>
        /// All Participants for this process
        /// </summary>
        private Dictionary<string, Invitee> _invitees { get; set; }

        private OrganiserProcess _organiserProcess { get; set; }

        private OrganiserUoW _organiserUoW { get; set; }

        private ILog logger = log4net.LogManager.GetLogger(typeof(Organiser));
        
        public Organiser( string organiserID/*, OrganiserProcess organiserProcess, Dictionary<string, Invitee> invitees */) 
        {
            OrganiserID = organiserID;
            this._organiserUoW = new OrganiserUoW();
            this._invitees = new Dictionary<string, Invitee>();
            try
            {
                this._organiserProcess = this._organiserUoW.OrganiserProcessRepository.Get(op => op.OrganiserID == OrganiserID).FirstOrDefault();
                foreach (Invitee inv in this._organiserUoW.InviteeRepository.GetInviteeByOrganiserID(organiserID))
                {
                    this._invitees.Add(inv.InviteeID, inv);
                }
            }
            catch(Exception)
            {

            }
        }

        /// <summary>
        /// To start organising a meeting the constructor must be called
        /// </summary>
        /// <param name="participants">All of the invited attendees</param>
        /// <param name="dateRequested">When the organiser was started</param>
        /// <param name="deadLineInDays">This is the deadline</param>
        /// <param name="requestedById">Who requested to organise this meeting ID</param>
        public Organiser(IEnumerable<InvitedParticipant> participants, DateTime dateRequested, DateIndex deadLineInDays, int requestedById, double duration)
        {
            //Init organiserUoW
            this._organiserUoW = new OrganiserUoW();

            //Create unique ID
            OrganiserID = DateHash();

            this._organiserProcess = new OrganiserProcess( OrganiserID, requestedById );
            this._organiserUoW.OrganiserProcessRepository.Insert(this._organiserProcess);
            this._organiserUoW.Save();
            

            //Set the duration
            this._organiserProcess.Duration = duration;

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

        /// <summary>
        /// This method will convert the invited participants to invitees.
        /// </summary>
        /// <param name="invitedParticipants"></param>
        /// <returns>boolean</returns>
        public Boolean ConvertParticipantsToInvitees(IEnumerable<InvitedParticipant> invitedParticipants)
        {
            this._organiserProcess.Status = OrganiserStatus.ConvertingParticipants;
            if( this._invitees == null)
            {
                this._invitees = new Dictionary<string, Invitee>();
            }
            try
            {
                foreach (var invitedParticipant in invitedParticipants)
                {
                    Invitee invitee = new Invitee(OrganiserID, invitedParticipant.id, invitedParticipant.Important);
                    this._invitees.Add(invitee.InviteeID, invitee);
                    this._organiserUoW.InviteeRepository.Insert(invitee);
                    this._organiserProcess.TotalInviteesUnanswered++;
                    this._organiserProcess.TotalInvitees++;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                this._organiserUoW.Save();
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

        /// <summary>
        /// This method will change the reserved entry, first add a new one then remove the old one
        /// </summary>
        /// <param name="ReservedSpotGuid"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void ChangeReservedSpot(Guid ReservedSpotGuid, DateTime start, DateTime end)
        {
            ReservedSpot r = Startup.SpotManagerFactory.GetReservedSpot(ReservedSpotGuid);
            ReservedSpot rnew = new ReservedSpot(){DateRange = new DateRange(start, end), Guid = ReservedSpotGuid};
            Startup.SpotManagerFactory.ChangeReservedSpot(r.DateRange, rnew);
        }
        
        /// <summary>
        /// This method will calculate the earliest appointment possible.
        /// </summary>
        private void CalculateEarliestProposition()
        {
            this._organiserProcess.Status = OrganiserStatus.CalculatingEarliestProposition;
            UpdateOrganiserProcess();
            //TODO: Add more room logic
            //HACK: Refactor Person available logic.
            //This is our start proposal daterange
            ProposalDateRange proposalDateRange = new ProposalDateRange(DateTime.Now, DateTime.Now.AddSeconds(this._organiserProcess.Duration));
            Room proposalRoom = null;

            //A reserved spot will be added to the list to reserve this proposition.
            ReservedSpot reservedSpot = new ReservedSpot();
            reservedSpot.DateRange = proposalDateRange;
            Guid ReservedSpotGuid = reservedSpot.Guid;
            //TODO: change key reservedspot when daterange changes
            Startup.SpotManagerFactory.AddSpot(reservedSpot);

            //A new proposel to send to the invitees will be created
            Proposition proposition = new Proposition(ReservedSpotGuid);

            //Try to make a proposal
            try
            {
                proposalDateRange = GetProposalDate(proposalDateRange, proposition.ReservedSpotGuid);
                proposalRoom = GetProposalRoom(proposalDateRange, proposition.ReservedSpotGuid);

                //Now let's create the proposal.
                Proposition p = new Proposition(reservedSpot.Guid);
                p.ProposedRoom = proposalRoom;
                p.BeginTime = proposalDateRange.Start;
                p.EndTime = proposalDateRange.End;
                this._organiserUoW.PropositionRepository.Insert(p);
                foreach (Invitee inv in this._invitees.Values)
                {
                    try
                    {
                        inv.Proposal = p;
                        this._organiserUoW.InviteeRepository.Update(inv);
                    }
                    catch(Exception)
                    {
                        
                    }
                }
                this._organiserUoW.Save();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in item.ValidationErrors)
                    {
                        logger.Error(error.ErrorMessage);
                    }
                }
            }
            catch (Exception)
            {
                this._organiserProcess.Status = OrganiserStatus.Error;
                UpdateOrganiserProcess();
                return;
                //Something went wrong while find a room
                //TODO: Logging
            }
            this._organiserProcess.Status = OrganiserStatus.WaitingOnResponses;
            UpdateOrganiserProcess();
        }

        /// <summary>
        /// This method will check if all invitees are able to come to a meeting at a certain time.
        /// </summary>
        /// <param name="prop">The current proposalDateRange</param>
        /// <param name="reservedSpot">The reserved spot Guid</param>
        /// <returns>DateRange</returns>
        private ProposalDateRange GetProposalDate(ProposalDateRange prop, Guid reservedSpot)
        {
            ProposalDateRange proposalDateRange = prop;

            SortedList<DateRange, PersonBlackSpot> pbss = Startup.SpotManagerFactory.GetPersonBlackSpots(this.OrganiserID);
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
                            //TODO: Add to check reserved persons..
                        }
                    }
                    catch (PersonNotAvailableException)
                    {
                        //Set the end time from the overlapping daterange as the start time for the proposalDateRange
                        proposalDateRange.ModifyStartDateTime(personBlackSpotDateRange.End, this._organiserProcess.Duration);
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

        /// <summary>
        /// Get a room for a specific datarange
        /// </summary>
        /// <param name="proposalDateRange"></param>
        /// <param name="reservedSpot"></param>
        /// <returns></returns>
        private Room GetProposalRoom(ProposalDateRange proposalDateRange, Guid reservedSpot) 
        {
            SortedList<DateRange, RoomBlackSpot> rbss = Startup.SpotManagerFactory.GetRoomBlackSpots();
            SortedList<DateRange, ReservedSpot> rss = Startup.SpotManagerFactory.GetReservedSpots();

            Room proposalRoom = null;
            //TODO: determine best location
            IEnumerable<Room> rooms = this._organiserUoW.RoomRepository.Get();

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

                        //If we reach this line the room is ok!, let's break out the loop
                        break;
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
                else
                {
                    return proposalRoom;
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
                return this._organiserUoW.UserProfileRepository.GetByID(userProfileId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Is the meeting ready?
        /// </summary>
        /// <returns></returns>
        public Boolean IsTheMeetingReadyToGo()
        {
            /*
            if(this.LocalInviter.DidAllInviteesAccept())
            {
                return true;
            }*/
            return false;
        }

        /// <summary>
        /// Register the response of the invitee.
        /// </summary>
        /// <param name="model">Contains the Organiser ID and invitee id</param>
        /// <returns>Boolean</returns>
        public Boolean registerAvailabilityInvitee(PropositionAnswerBindingModel model)
        {
            //HACK: Change the propositionAnswerBindingModel
            if (this._invitees.ContainsKey(model.InviteeID))
            {
                this._invitees[model.InviteeID].Answer = model.Answer;
                this._organiserUoW.InviteeRepository.Update(this._invitees[model.InviteeID]);
                this._organiserUoW.Save();
                CheckAnswer(model.InviteeID);
            }

            //TODO:Should this be a new feature? HowManyCantCome()
            //Notify the Creator if to many non-imporant people can't come.
            //Let's say that if X% of the people did not accept it will send a notice to the creator if the meeting should continue
            //Or even another deadline

            return false;
        }

        /// <summary>
        /// Check what the invitee response was
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Boolean CheckAnswer(string id)
        {
            bool returnVal = false;
            //TODO: If their is send a new proposition reduce Totals!
            if (this._invitees[id].Important == true && this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Reschudele meeting
                //Let the System know that it need to reschedule everything
                //But add a blackspot to the existing list

                this._organiserProcess.TotalImporantInviteesAbsent++;
                this._organiserProcess.TotalInviteesAbsent++;
            }
            else if (this._invitees[id].Answer == Availability.Absent)
            {
                //TODO:Let the system decide if their is a new proposition needed.
                //If their is no new proposition the meeting will continue (unless he joins online)

                this._organiserProcess.TotalInviteesAbsent++;

            }
            else if ( this._invitees[id].Answer == Availability.Online )
            {
                //TODO:Proposition may not be online at the first try
                //Example: Could you be present at this meeting Present/Absent
                //When the person chose Absent they get another message 
                //from the system asking if they want to be present Online
                //Increase the OnlineCounter for this organisation. 
                this._organiserProcess.TotalInviteesOnline++;
                returnVal = true;
            }
            this._organiserProcess.TotalInviteesUnanswered--;
            UpdateOrganiserProcess();
            return returnVal;

        }

        /// <summary>
        /// Get the status of the organiser
        /// </summary>
        /// <returns></returns>
        public OrganiserResponse GetStatus()
        {
            //TODO: Get the status of the organiser
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the proposition for a invitee
        /// </summary>
        /// <param name="inviteeID"></param>
        /// <returns></returns>
        public Proposition GetProposition(string inviteeID)
        {
            throw new NotImplementedException();
        }


        private void UpdateOrganiserProcess()
        {
            try
            {
                this._organiserUoW.OrganiserProcessRepository.Update(this._organiserProcess);
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                this._organiserUoW.Save();
            }
        }
    }

    /// <summary>
    /// The model of for inviting a participant
    /// </summary>
    public class InvitedParticipant
    {
        [Required]
        public int id { get; set; }

        [Required]
        public Boolean Important { get; set; }
    }
}