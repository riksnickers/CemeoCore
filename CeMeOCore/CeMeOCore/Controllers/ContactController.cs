using CeMeOCore.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers
{
    public class ContactController : ApiController
    {
        private ContactUoW _contactUoW;

        public ContactController()
        {
            this._contactUoW = new ContactUoW();
        }

        /// <summary>
        /// This method will return contacts.
        /// This is a GET method
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("Compact")]
        public IEnumerable<Object> GetContacts()
        {
            return this._contactUoW.UserProfileRepository.GetContactsCompact();
        }

        protected override void Dispose(bool disposing)
        {
            this._contactUoW.Dispose();
            base.Dispose(disposing);
        }
    }
}
