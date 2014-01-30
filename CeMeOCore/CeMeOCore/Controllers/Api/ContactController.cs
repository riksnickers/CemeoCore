using CeMeOCore.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers
{
    /// <summary>
    /// The WebAPI Contact controller. This will handle all contact actions
    /// </summary>
    [RoutePrefix("api/Contact")]
    public class ContactController : ApiController
    {
        /// <summary>
        /// This is the private property for the Contact Unit of Work
        /// </summary>
        private ContactUoW _contactUoW;

        /// <summary>
        /// The constructor will initialize the Unit of Work
        /// </summary>
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

        /// <summary>
        /// Dispose the Controller + Unit of work
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (this._contactUoW != null)
            {
                this._contactUoW.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
