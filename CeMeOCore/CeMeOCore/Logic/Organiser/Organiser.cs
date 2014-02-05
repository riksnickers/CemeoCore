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
using CeMeOCore.Logic.PushNotifications;
using CeMeOCore.Logic.Exchange;
using System.Collections;


namespace CeMeOCore.Logic.Organiser
{
    /// <summary>
    /// The organiser will organise a meeting
    /// </summary>
    public class Organiser : IOrganiser
    {
        public event GenerateBlackSpots GenerateBlackSpotss;
        public delegate bool GenerateBlackSpots(string organiserID);

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
        
        public Organiser( string organiserID ) 
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
                ResolveInvitees();
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

            this._organiserProcess = new OrganiserProcess( OrganiserID, requestedById, deadLineInDays );
            this._organiserUoW.OrganiserProcessRepository.Insert(this._organiserProcess);
            this._organiserProcess.DeadLineInDays = deadLineInDays;
            this._organiserUoW.Save();
            

            //Set the duration
            this._organiserProcess.Duration = duration;

            //Resolve invitedParticipants
            ConvertParticipantsToInvitees(participants);
            ResolveInvitees();
           
            //CheckAttendeesCalendar/appointments
            CheckAvailabilityInvitees();

            //  CalculateEarliestAppointment - take blackspots in account
            CalculateEarliestProposition();

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
            foreach(Invitee i in this._invitees.Values)
            {
                ExchangeImpl ex = new ExchangeImpl(i.User.UserName, "jefjef91", "cemeo.be", i.UserID);
                ex.GenerateBlackSpots(OrganiserID, i.User);
            }
        }

        private void ResolveInvitees()
        {
            foreach(Invitee invitee in this._invitees.Values)
            {
                invitee.User = ResolveUser(invitee.UserID);
            }
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
            ReservedSpot rnew = new ReservedSpot();
            rnew = r;
            rnew.DateRange = new DateRange(start, end);
            rnew.Guid = ReservedSpotGuid;
            Startup.SpotManagerFactory.ChangeReservedSpot(r.DateRange, rnew);
        }
        
        private DateTime GetDeadlineDate()
        {
            //HACK: Refactor this so it gets better (take weekends in count)..
            DateTime now = DateTime.Now;
            switch( this._organiserProcess.DeadLineInDays )
            {
                case DateIndex.Today:
                    return new DateTime(now.Year, now.Month, now.Day, 23, 00, 00);
                case DateIndex.WithinThisWorkWeek:
                case DateIndex.Within7Days:
                    return new DateTime(now.Year, now.Month, now.Day, 23, 00, 00).AddDays(6);
                case DateIndex.Within30Days:
                case DateIndex.WithinThisMonth:
                    return new DateTime(now.Year, now.Month, now.Day, 23, 00, 00).AddDays(29);
                default:
                    return new DateTime(now.Year, now.Month, now.Day, 23, 00, 00).AddDays(2);   
            }
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
            DateTime now = DateTime.Now;
            DateTime temp = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddHours(1);
            ProposalDateRange proposalDateRange = new ProposalDateRange(temp, temp.AddSeconds(this._organiserProcess.Duration));
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
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\n\t OrganiserID: " + OrganiserID + "\n\tGetProposalDate" + "\n\tProposalDateRange: " + proposalDateRange.Start + "-" + proposalDateRange.End );
                proposalDateRange = GetProposalDate(proposalDateRange, proposition.ReservedSpotGuid);
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\n\t OrganiserID: " + OrganiserID + "\n\tGetProposalDate" + "\n\tNew - ProposalDateRange: " + proposalDateRange.Start + "-" + proposalDateRange.End);
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\n\t OrganiserID: " + OrganiserID + "\n\tGetProposalRoom");
                proposalRoom = GetProposalRoom(proposalDateRange, proposition.ReservedSpotGuid);

                //Now let's create the proposal.
                Proposition p = new Proposition(reservedSpot.Guid);
                p.OrganiserProcess = this._organiserProcess;
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
            catch (Exception ex)
            {
                this._organiserProcess.Status = OrganiserStatus.Error;
                UpdateOrganiserProcess();
                logger.Error(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\t" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                return;
                //Something went wrong while find a room
                //TODO: Logging
            }
            this._organiserProcess.Status = OrganiserStatus.WaitingOnResponses;
            UpdateOrganiserProcess();
            sendPropositionToInvitees();
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

            Dictionary<int, SortedList<DateRange, PersonBlackSpot>> pbss = Startup.SpotManagerFactory.GetPersonBlackSpots(this.OrganiserID);
            //Try to find a good daterange looking at persons
            try
            {
                foreach (int userBlackSpotKey in pbss.Keys)
                {
                    try
                    {
                        if(FindSpotOverlaps(pbss[userBlackSpotKey].Values, ref proposalDateRange))
                        {
                            throw new PersonNotAvailableException();
                        }
                        else
                        {
                            bool doOthersHaveOverlaps = false;
                            //Check other
                            foreach(int otherUserSpotKey in pbss.Keys)
                            {
                                if( otherUserSpotKey != userBlackSpotKey )
                                {
                                    //so it does not overwrite our good proposaldaterange
                                    ProposalDateRange temp = proposalDateRange;
                                    doOthersHaveOverlaps = FindSpotOverlaps(pbss[userBlackSpotKey].Values, ref temp);
                                    if(doOthersHaveOverlaps)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (doOthersHaveOverlaps)
                            {
                                continue;
                            }
                            else
                            {
                                ChangeReservedSpot(reservedSpot, proposalDateRange.Start, proposalDateRange.End);
                                break;
                            }
                        }
                    }
                    catch (PersonNotAvailableException)
                    {
                        //This person is not available.. check if it is an important person
                        //if to many absent then throw NoPersonsAvailableException
                        continue;
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
                //Handle here what needs to be done when a user is not available
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\n\t OrganiserID: " + OrganiserID + "\n\tNoPersonsAvailableException..");
            }
            catch (Exception ex)
            {
                //Throw it up!
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(Organiser) + "\n\t OrganiserID: " + OrganiserID + "\n\tException in proposalDate! " + ex.Message + "\n\t" + ex.Source + "\n\t" + ex.StackTrace);
                throw;
            }
            return proposalDateRange;
        }

        private bool FindSpotOverlaps(IList<PersonBlackSpot> values, ref ProposalDateRange dr)
        {
            bool overlap = false;
            try
            {
                foreach (PersonBlackSpot spot in values)
                {
                    //Check if the proposal start or end time overlaps with the spot start or end time.
                    logger.Debug("******* dr.Start:" + dr.Start + " -- spot.Start: " + spot.DateRange.Start + "\n**** dr.End" + dr.End + " -- spot.End: " + spot.DateRange.End);
                    logger.Debug("******* !(spot.DateRange.Includes(dr.Start)) = " + !(spot.DateRange.Includes(dr.Start)) + " OR " + " ******* !(spot.DateRange.Includes(dr.Start)) = " + !(spot.DateRange.Includes(dr.Start)));
                    if (!(spot.DateRange.Includes(dr.Start)) || !(spot.DateRange.Includes(dr.Start)))
                    {
                        //if one of the two includes in the other 
                        //Set the end time from the overlapping daterange as the start time for the proposalDateRange
                        if (spot.DateRange.End >= DateTime.Now.AddMinutes(30))
                        {
                            dr.ModifyStartDateTime(spot.DateRange.End.AddMinutes(5), this._organiserProcess.Duration);
                            overlap = true;
                            continue;
                        }
                        //logger.Debug("******* dr.Start:" + dr.Start + " -- spot.Start: " + spot.DateRange.Start + "\n**** dr.End" + dr.End + " -- spot.End: " + spot.DateRange.End)
           
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(PersonNotAvailableException)
            {
                //The person is not available.. but throw it up
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
            return overlap;
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
            logger.Info(DateTime.Now + " - " + "Got all the rooms: " + rooms.Count());
            //Let's try to find a room
            try
            {
                //Now let's check if their is a room available
                //Get a room and check if it's available.
                foreach (Room room in rooms)
                {
                    proposalRoom = room;
                    logger.Info(DateTime.Now + " - " + "Got a room " + proposalRoom.RoomID);
                    try
                    {
                        logger.Info(DateTime.Now + " - " + "Number of room blackspots: " + rbss.Values.Count());
                        //enumerate all roomblackspots
                        foreach (RoomBlackSpot roomSpot in rbss.Values)
                        {
                            logger.Info(DateTime.Now + " - " + "Check if room " + proposalRoom.RoomID + " is available.. with blackspot: " + roomSpot.Room.RoomID + " - " + roomSpot.BegintTime + "-" + roomSpot.EndTime);
                            logger.Info(DateTime.Now + " - " + (roomSpot.Room.RoomID == proposalRoom.RoomID) + " .. " + roomSpot.Room.RoomID + " - " + proposalRoom.RoomID);
                            //Check if the spot overlaps with the proposaldaterange
                            if (roomSpot.Room.RoomID == proposalRoom.RoomID)
                            {
                                logger.Info(DateTime.Now + " - " + "Found a blackspot for this room!\n" + (roomSpot.DateRange.Includes(proposalDateRange.Start)) + " OR " + (roomSpot.DateRange.Includes(proposalDateRange.Start)));
                                if ((roomSpot.DateRange.Includes(proposalDateRange.Start)) || (roomSpot.DateRange.Includes(proposalDateRange.Start)))
                                {
                                    logger.Info(DateTime.Now + " - " + "Room not available..");
                                    throw new RoomNotAvailableException();
                                }
                            }
                        }
                        /*
                        //Now check if the proposalroom is reserved at the moment
                        //First check if a daterange overlaps with the proposal daterange
                        foreach (ReservedSpot reservedspot in rss.Values)
                        {
                            //We have found a overlap in the daterange, is it the room we are looking for?
                            //Does the reservedSpot includes the room And it's not our reserved spot.
                            if (reservedspot.Includes(proposalRoom))
                            {
                                throw new RoomIsReservedException();
                            }
                        }
                        */
                        //If we reach this line the room is ok!, let's break out the loop
                        break;
                    }
                    catch (RoomNotAvailableException)
                    {
                        //It's the room we are looking for; but it's not available 
                        //We have a NO GO for this room!
                        //Let's continue looking for a room
                        proposalRoom = null;
                        continue;
                    }
                    catch (RoomIsReservedException)
                    {
                        //The room is reserved for an other meeting at the same time.
                        //We have a NO GO for this room! but this room might be available later
                        //TODO: add listener when rooms becomes available..?
                        proposalRoom = null;
                        continue;
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
                    logger.Info("We did not found the room :( " + proposalRoom.RoomID);
                    throw new NoRoomsFoundException();
                }
                else
                {
                    logger.Info("We found the room: " + proposalRoom.RoomID);
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
            foreach(Invitee invitee in this._invitees.Values)
	        {
		        if(invitee.Answer != Availability.Absent )
                {
                    foreach(Device device in DeviceManager.GetDevicesFromUser(invitee.UserID))
                    {
                        PushContext pc = new PushContext();
                        pc.Send(device, "You have a new proposition waiting on you ;)!");
                    }

                }
	        }
            
        }

        /// <summary>
        /// This will resolve the id of the creator
        /// </summary>
        /// <param name="userProfileId">The id of who requested the meeting
        /// </param>
        /// <returns></returns>
        private UserProfile ResolveUser( int userProfileId )
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
        public void registerAvailabilityInvitee(PropositionAnswerBindingModel model)
        {
            //TODO: if meeting is created other procedure
            //HACK: Change the propositionAnswerBindingModel
            if (this._invitees.ContainsKey(model.InviteeID))
            {
                CheckAnswer(model.InviteeID, model.Answer);
                this._invitees[model.InviteeID].Answer = model.Answer;
                this._organiserUoW.InviteeRepository.Update(this._invitees[model.InviteeID]);
                this._organiserUoW.Save();
            }

            //TODO:Should this be a new feature? HowManyCantCome()
            //Notify the Creator if to many non-imporant people can't come.
            //Let's say that if X% of the people did not accept it will send a notice to the creator if the meeting should continue
            //Or even another deadline
        }

        /// <summary>
        /// Check what the invitee response was
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private void CheckAnswer(string id, Availability answer)
        {
            //Check if the user changed his/her answer
            
            switch(this._invitees[id].Answer)
            {
                case Availability.Absent:
                    this._organiserProcess.TotalInviteesAbsent--;
                    this._organiserProcess.TotalInviteesUnanswered++;
                    break;
                case Availability.Online:
                    this._organiserProcess.TotalInviteesOnline--;
                    this._organiserProcess.TotalInviteesUnanswered++;
                    break;
                case Availability.Present:
                    this._organiserProcess.TotalInviteesUnanswered++;
                    break;
                case Availability.Unanswered:
                    break;
            }

            switch(answer)
            {
                case Availability.Absent:
                    if (this._invitees[id].Important)
                    {
                        //TODO:Reschudele meeting
                        //Let the System know that it need to reschedule everything
                        //But add a blackspot to the existing list

                        this._organiserProcess.TotalImporantInviteesAbsent++;
                    }
                    //TODO:Let the system decide if their is a new proposition needed.
                    //If their is no new proposition the meeting will continue (unless he joins online)
                    this._organiserProcess.TotalInviteesAbsent++;
                    this._organiserProcess.TotalInviteesUnanswered--;
                    break;

                case Availability.Online:
                    //TODO:Proposition may not be online at the first try
                    //Example: Could you be present at this meeting Present/Absent
                    //When the person chose Absent they get another message 
                    //from the system asking if they want to be present Online
                    //Increase the OnlineCounter for this organisation. 
                    this._organiserProcess.TotalInviteesOnline++;
                    this._organiserProcess.TotalInviteesUnanswered--;
                    break;

                case Availability.Present:
                    this._organiserProcess.TotalInviteesUnanswered--;
                    break;
            }

            if( this._organiserProcess.TotalInviteesUnanswered == 0 )
            {
                OrganiseMeeting();
            }

            UpdateOrganiserProcess();
        }

        public void OrganiseMeeting()
        {
            try
            {
                Meeting newMeeting = new Meeting();
                newMeeting.BeginTime = this._invitees.First().Value.Proposal.BeginTime;
                newMeeting.Duration = this._organiserProcess.Duration;

                this._organiserUoW.MeetingRepository.Insert(newMeeting);
                //Save the context so the meeting is getting an id
                this._organiserUoW.Save();

                foreach (Invitee invitee in this._invitees.Values)
                {
                    Attendee attendee = new Attendee();
                    attendee.MeetingId = newMeeting.MeetingID;
                    attendee.Room = invitee.Proposal.ProposedRoom;
                    attendee.UserId = invitee.UserID;
                    newMeeting.Attendees.Add(attendee);
                    this._organiserUoW.AttendeeRepository.Insert(attendee);
                }

                this._organiserUoW.MeetingRepository.Update( newMeeting );


                this._organiserUoW.Save();

                RoomBlackSpot LastAddedRoom = null;
             
                foreach( Invitee invitee in this._invitees.Values)
                {
                    ExchangeImpl ex = new ExchangeImpl(invitee.User.UserName, "jefjef91", "cemeo.be", invitee.UserID);
                    ex.CreateAppointment(newMeeting, invitee.Proposal.ProposedRoom, this._invitees.Values.ToList());
                    if(LastAddedRoom == null)
                    {
                        logger.Info(DateTime.Now + " - " + "Adding first room");
                        LastAddedRoom = new RoomBlackSpot(newMeeting.BeginTime, newMeeting.BeginTime.AddSeconds(newMeeting.Duration), invitee.Proposal.ProposedRoom);
                        Startup.SpotManagerFactory.AddSpot(LastAddedRoom);
                    }
                    logger.Info(DateTime.Now + " - " + "LastAddedRoom " + LastAddedRoom.Room.RoomID + " - InviteeRoom " + invitee.Proposal.ProposedRoom.RoomID);
                    if (LastAddedRoom.Room.RoomID != invitee.Proposal.ProposedRoom.RoomID)
                    {
                        logger.Info(DateTime.Now + " - " + "Adding last room");
                        LastAddedRoom = new RoomBlackSpot(newMeeting.BeginTime, newMeeting.BeginTime.AddSeconds(newMeeting.Duration), invitee.Proposal.ProposedRoom);
                        Startup.SpotManagerFactory.AddSpot(LastAddedRoom);
                    }
                }
            }
            catch( Exception ex )
            {
                logger.Error(DateTime.Now + " Class: " + typeof(Organiser) + " - \n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace );
            }

            this._organiserProcess.Status = OrganiserStatus.FinishedOrganising;
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
                OrganiserProcess o = this._organiserProcess;
                this._organiserUoW.OrganiserProcessRepository.Update(o);
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

        public bool IncludesInvitee(string inviteeID)
        {
            return this._invitees.Keys.Contains(inviteeID);
        }

        public event EventHandler DeadLineChanged;
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