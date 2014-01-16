using CeMeOCore.DAL.Models;
using log4net;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CeMeOCore.DAL.UnitsOfWork;

namespace CeMeOCore.Controllers
{
    [Authorize]
    [RoutePrefix("api/Proposition")]
    public class PropositionController : ApiController
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(PropositionController));

        private PropositionControllerUoW _propositionUoW;

        public PropositionController()
        {
            this._propositionUoW = new PropositionControllerUoW();
        }

        /// <summary>
        /// When the server sends a pushnotification/payload
        /// it includes an ID that identifies Which inviter is being used.
        /// </summary>
        /// <param name="model"><seealso cref="PropositionAnswerBindingModel"/></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("PropositionAnswer")]
        public IHttpActionResult InviteResponse([FromBody]PropositionAnswerBindingModel model)
        {
            Startup.OrganiserManagerFactory.NotifyOrganiser(model);
            return Ok();
        }

        /// <summary>
        /// Get all propositions for the logged in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("Propositions")]
        public IEnumerable<Proposition> GetPropositions(GetPropositionBindingModel model)
        {
            //Get UserProfileID
            string aspID = User.Identity.GetUserId();
            int upID = this._propositionUoW.UserProfileRepository.Get(u => u.aspUser == aspID).Select(u => u.UserId).First();


            HashSet<Proposition> propositions = new HashSet<Proposition>();

            foreach (Invitee invitee in this._propositionUoW.InviteeRepository.GetInviteeIDsByUserProfileID(upID))
            {
                Proposition p = invitee.GetProposition();
                if (p != null)
                {
                    propositions.Add(p);
                }
            }
            return propositions;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}