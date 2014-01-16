using CeMeOCore.DAL.Context;
using CeMeOCore.DAL.Repositories;
using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.UnitsOfWork
{
    /// <summary>
    /// The unit of work used in the AccountController.
    /// </summary>
    public class UserUoW : IDisposable
    {
        /// <summary>
        /// The initialization of the context.
        /// </summary>
        private CeMeoContext context = new CeMeoContext();
        /// <summary>
        /// The needed repositories
        /// </summary>
        private UserProfileRepository _userProfileRepository;
        private GenericRepository<Location> _locationRepository;
        private InviteeRepository _inviteeRepository;

        /// <summary>
        /// The UserProfileRepository property
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
        /// The LocationRepository property
        /// </summary>
        public GenericRepository<Location> LocationRepository
        {
            get
            {
                if( this._locationRepository == null )
                {
                    this._locationRepository = new GenericRepository<Location>(context);
                }
                return this._locationRepository;
            }
        }

        /// <summary>
        /// The InviteeRepository property
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
        /// Save the context
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        /// <summary>
        /// Disposes the context
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
        /// Dispose function
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}