using CeMeOCore.DAL.Repositories;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.UnitsOfWork
{
    /// <summary>
    /// This is the Unit of Work for the ContactController
    /// </summary>
    public class ContactUoW : IDisposable
    {
        /// <summary>
        /// Initialize the context
        /// </summary>
        private CeMeoContext context = new CeMeoContext();
        /// <summary>
        /// The repository needed
        /// </summary>
        private UserProfileRepository _userProfileRepository;

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
        /// Save the context
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
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

        //Dispose method
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}