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
    /// This is the Unit of Work for the LocationController
    /// </summary>
    public class RoomUoW : IDisposable
    {
        /// <summary>
        /// The initialization of the context.
        /// </summary>
        private CeMeoContext context = new CeMeoContext();
        /// <summary>
        /// The repository needed here
        /// </summary>
        private GenericRepository<Room> _roomRepository;

        /// <summary>
        /// The GenericRepository initialized with Location 
        /// </summary>
        public GenericRepository<Room> roomnRepository
        {
            get
            {

                if (this._roomRepository == null)
                {
                    this._roomRepository = new GenericRepository<Room>(context);
                }
                return this._roomRepository;
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