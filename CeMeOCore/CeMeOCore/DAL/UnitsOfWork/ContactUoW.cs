using CeMeOCore.DAL.Repositories;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.UnitsOfWork
{
    public class ContactUoW : IDisposable
    {
        private CeMeoContext context = new CeMeoContext();
        private UserProfileRepository _userProfileRepository;

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