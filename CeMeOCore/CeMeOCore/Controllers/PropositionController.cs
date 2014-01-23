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
using CeMeOCore.Logic.Organiser;
using CeMeOCore.Logic.PushNotifications;

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
        [Route("Answer")]
        public IHttpActionResult InviteResponse([FromBody]PropositionAnswerBindingModel model)
        {
            OrganiserManager.NotifyOrganiser(model);
            return Ok();
        }

        /// <summary>
        /// Get all propositions for the logged in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("All")]
        public IEnumerable<ExtendenProposition> GetPropositions()
        {
            //Get UserProfileID
            string aspID = User.Identity.GetUserId();
            int upID = this._propositionUoW.UserProfileRepository.Get(u => u.aspUser == aspID).Select(u => u.UserId).First();


            HashSet<ExtendenProposition> propositions = new HashSet<ExtendenProposition>();

            foreach (Invitee invitee in this._propositionUoW.InviteeRepository.GetInviteeIDsByUserProfileID(upID))
            {
                ExtendenProposition extendedProposition = new ExtendenProposition();

                extendedProposition.InviteeID = invitee.InviteeID;
                extendedProposition.Proposition = invitee.GetProposition();
                extendedProposition.Answer = invitee.Answer;
                //Add all other intitees to the return
                foreach (Invitee other in this._propositionUoW.InviteeRepository.GetInviteeByOrganiserID(invitee.OrganiserID))
                {
                    if(other.UserID != invitee.UserID)
                    {
                        extendedProposition.Others.Add(this._propositionUoW.UserProfileRepository.GetByIDCompact(other.UserID));
                    }
                }
                if (extendedProposition != null && extendedProposition.Proposition != null)
                {
                    propositions.Add(extendedProposition);
                }
            }
            return propositions;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("push")]
        public string GetPush()
        {
            Device d = new Device();
            d.DeviceID = "fc0e50bb4200be9a719e1650c01171d72ec8d9d29c39acc0e14ae8638bc5e4c4";
            d.Platform = Platform.Apple;
            d.userID = 1;

            PushContext pc = new PushContext();
            pc.Send(d, "Hoi jef!");
            return "test";
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