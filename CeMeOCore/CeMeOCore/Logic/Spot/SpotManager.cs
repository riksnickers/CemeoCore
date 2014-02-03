using CeMeOCore.Logic.Range;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.Spots
{
    /// <summary>
    /// The spotManager holds all the spots
    /// Needs to be converted to a repository
    /// </summary>
    public class SpotManager
    {
        /// <summary>
        /// Private SortedLists and a dictionary for saving the spots.
        /// </summary>
        private Dictionary<String, SortedList<DateRange, PersonBlackSpot>> _organiserPersonSpots; //Nested collection Dictionary + SortedList
        private SortedList<DateRange, RoomBlackSpot> _roomSpots;
        private SortedList<DateRange, ReservedSpot> _reservedSpots;
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(SpotManager));
        /// <summary>
        /// The constructors initializes all collections ( but not the nested SortedList )
        /// </summary>
        public SpotManager()
        {
            this._organiserPersonSpots = new Dictionary<string, SortedList<DateRange, PersonBlackSpot>>();
            this._roomSpots = new SortedList<DateRange, RoomBlackSpot>(new DateRange.Comparer());
            this._reservedSpots = new SortedList<DateRange, ReservedSpot>(new DateRange.Comparer());
        }

        /// <summary>
        /// Get the SortedList for an organiser.
        /// </summary>
        /// <param name="OrganiserID"></param>
        /// <returns></returns>
        public SortedList<DateRange, PersonBlackSpot> GetPersonBlackSpots(string OrganiserID)
        {
            if (!this._organiserPersonSpots.ContainsKey(OrganiserID))
            {
                this._organiserPersonSpots.Add(OrganiserID, new SortedList<DateRange, PersonBlackSpot>(new DateRange.Comparer()));
            }
            return this._organiserPersonSpots[OrganiserID];
        }

        /// <summary>
        /// Get the SortedList of the roomBlackSpots
        /// </summary>
        /// <returns></returns>
        public SortedList<DateRange, RoomBlackSpot> GetRoomBlackSpots()
        {
            return this._roomSpots;
        }

        /// <summary>
        /// Get the SortedList of the ReservedSpots
        /// </summary>
        /// <returns></returns>
        public SortedList<DateRange, ReservedSpot> GetReservedSpots()
        {
            return this._reservedSpots;
        }
        
        /// <summary>
        /// Get a specific reservedSpot
        /// </summary>
        /// <param name="guid">Guid of the spot</param>
        /// <returns></returns>
        public ReservedSpot GetReservedSpot(Guid guid)
        {
            return this._reservedSpots.Where(s => s.Value.Guid == guid).FirstOrDefault().Value;
        }

        /// <summary>
        /// Add a spot to the correct list
        /// </summary>
        /// <param name="spot"></param>
        public void AddSpot(ISpot spot)
        {
            try
            {
                if (spot is RoomBlackSpot)
                {
                    this._roomSpots.Add(spot.DateRange, (RoomBlackSpot)spot);
                    logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(SpotManager) + "\t" + "Successful added Spot" + "\n" + spot.DateRange.Start + "\n" + spot.DateRange.End);
                }
                else if (spot is PersonBlackSpot)
                {
                    //Convert spot to PersonBlackSpot so that the OrganiserID can be accessed
                    PersonBlackSpot pbs = (PersonBlackSpot)spot;
                    //Check if the OrganiserID is already a key of the OrganiserPersonSpots
                    if ( !this._organiserPersonSpots.ContainsKey(pbs.OrganiserID) )
                    {
                        //If not create a new SortedList for the organiser and add it to the dictionary
                        this._organiserPersonSpots.Add(pbs.OrganiserID, new SortedList<DateRange, PersonBlackSpot>(new DateRange.Comparer()));
                    }
                    //Now add the PersonBlackSpot to the correct list.
                    this._organiserPersonSpots[pbs.OrganiserID].Add(spot.DateRange, pbs);
                    logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(SpotManager) + "\t" + "Successful added Spot" + "\n DateRange: " + pbs.DateRange.Start + "\t" + pbs.DateRange.End  +"\n OrganiserId: "+ pbs.OrganiserID + "\n UserProfile: " + pbs.Person.UserId + " - " + pbs.Person.UserName);
                }
                else if (spot is ReservedSpot)
                {
                    this._reservedSpots.Add(spot.DateRange, (ReservedSpot)spot);
                    logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(SpotManager) + "\t" + "Successful added Spot" + "\n" + spot.DateRange.Start + "\n" + spot.DateRange.End);
                }
            }
            catch (Exception ex)
            {
                //TODO: add logging
                logger.Debug(DateTime.Now.ToString() + "\t" + "Class: " + typeof(SpotManager) + "\t" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw;
            }
            
        }

        /// <summary>
        /// Change the Reserved Spot.. Still a work in progress
        /// </summary>
        /// <param name="OldDR"></param>
        /// <param name="NewDR"></param>
        public void ChangeReservedSpot(DateRange OldDR, ReservedSpot NewDR)
        {
            try
            {
                if (this._reservedSpots.ContainsKey(OldDR))
                {
                    //If not exceptions on adding a spot let's now remove the old one.
                    this._reservedSpots.Remove(OldDR);
                    //Let's first add the new spot, if this fails the old spot will not be removed!
                    AddSpot(NewDR);
                }
            }
            catch (Exception)
            {
                //propably this will be already dupplicated Key exception
                throw;
            }
        }
    }
}