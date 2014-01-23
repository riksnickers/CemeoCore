using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Repositories;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using log4net;

namespace CeMeOCore.DAL.UnitsOfWork
{
    /// <summary>
    /// This is the Unit of Work for the Organiser Class
    /// </summary>
    public class OrganiserUoW : IDisposable
    {
        /// <summary>
        /// The initialization of the context.
        /// </summary>
        private CeMeoContext context = new CeMeoContext();
        /// <summary>
        /// The repositories used in the organiser
        /// </summary>
        private InviteeRepository _inviteeRepository;
        private PropositionRepository _propositionRepository;
        private RoomRepository _roomRepository;
        private UserProfileRepository _userProfileRepository;
        private OrganiserProcessRepository _organiserProcessRepository;
        private MeetingRepository _meetingRepository;

        //private OrganiserRepository _organiserRepository;

        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(OrganiserUoW));

        /// <summary>
        /// The InviteeRepository Property
        /// </summary>
        public InviteeRepository InviteeRepository
        {
            get
            {
                if (this._inviteeRepository == null)
                {
                    this._inviteeRepository = new InviteeRepository(context);
                }
                return this._inviteeRepository;
            }
        }

        /// <summary>
        /// The PropositionRepository Property
        /// </summary>
        public PropositionRepository PropositionRepository
        {
            get
            {
                if(this._propositionRepository == null )
                {
                    this._propositionRepository = new PropositionRepository(context);
                }
                return this._propositionRepository;
            }
        }

        /// <summary>
        /// The RoomRepository Property
        /// </summary>
        public RoomRepository RoomRepository
        {
            get
            {
                if(this._roomRepository == null)
                {
                    this._roomRepository = new RoomRepository(context);
                }
                return this._roomRepository;
            }
        }

        /// <summary>
        /// The UserProfileRepository Property
        /// </summary>
        public UserProfileRepository UserProfileRepository
        {
            get
            {
                if (this._userProfileRepository == null)
                {
                    this._userProfileRepository = new UserProfileRepository(context);
                }
                return this._userProfileRepository;
            }
        }

        /// <summary>
        /// The OrganiserProcessRepository Property
        /// </summary>
        public OrganiserProcessRepository OrganiserProcessRepository
        {
            get
            {
                if (this._organiserProcessRepository == null)
                {
                    this._organiserProcessRepository = new OrganiserProcessRepository(context);
                }
                return this._organiserProcessRepository;
            }
        }

        /// <summary>
        /// The MeetingRepository Property
        /// </summary>
        public MeetingRepository MeetingRepository
        {
            get
            {
                if (this._meetingRepository == null)
                {
                    this._meetingRepository = new MeetingRepository(context);
                }
                return this._meetingRepository;
            }
        }

        /// <summary>
        /// Save the context
        /// </summary>
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in item.ValidationErrors)
                    {
                        logger.Error(DateTime.Now.ToString() + " " + error.ErrorMessage);
                    }
                }
            }
            catch(Exception)
            {

            }
            
        }

        private bool disposed = false;

        /// <summary>
        /// Dispose the context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}