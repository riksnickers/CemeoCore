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
    /// Still in progress, no comments yet, please come back soon...
    /// Still thinking about the function of this UoW..
    /// Sorry..
    /// </summary>
    public class MeetingControllerUoW : IDisposable
    {
        private CeMeoContext context = new CeMeoContext();
        private UserProfileRepository _userProfileRepository;
        private AttendeeRepository _attendeeRepository;
        private MeetingRepository _meetingRepository;

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

        public AttendeeRepository AttendeeRepository
        {
            get
            {
                if(this._attendeeRepository == null)
                {
                    this._attendeeRepository = new AttendeeRepository(context);
                }
                return this._attendeeRepository;
            }
        }

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

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}