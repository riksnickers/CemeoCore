using CeMeOCore.DAL.Repositories;
using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.DAL.UnitsOfWork
{
    public class OrganiserUoW
    {
        private CeMeoContext context = new CeMeoContext();
        private InviteeRepository _inviteeRepository;
        private PropositionRepository _propositionRepository;

        public InviteeRepository UserProfileRepository
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