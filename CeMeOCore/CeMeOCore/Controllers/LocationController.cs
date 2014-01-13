using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers
{
    [Authorize]
    public class LocationController : ApiController
    {
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(LocationController));
        private LocationUoW _locationUoW;

        public LocationController()
        {
            this._locationUoW = new LocationUoW();
        }

        // GET api/Location
        [AcceptVerbs("GET")]
        public IEnumerable<Location> Get()
        {
            return this._locationUoW.LocationRepository.Get();
        }

        // GET api/values/5
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get(HttpRequestMessage mes, int id)
        {
            Location ll;
            try
            {
                ll = this._locationUoW.LocationRepository.GetByID(id);
            }
            catch (Exception)
            {

                return mes.CreateResponse(HttpStatusCode.NoContent);
            }
            return mes.CreateResponse(HttpStatusCode.Found, ll);

            
        }

        // POST api/values
        [AcceptVerbs("POST")]
        public void Post([FromBody]Location value)
        {
            try
            {
                this._locationUoW.LocationRepository.Insert(value);
                this._locationUoW.Save();
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        // PUT api/values/5
        [AcceptVerbs("PUT")]
        public void Put(int id, [FromBody]Location value)
        {
            try
            {
                this._locationUoW.LocationRepository.Update(value);
                this._locationUoW.Save();
            }
            catch(Exception)
            {
                throw;
            }
        }

        // DELETE api/values/5
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            try
            {
                var loc = this._locationUoW.LocationRepository.GetByID(id);
                this._locationUoW.LocationRepository.Delete(loc);
                this._locationUoW.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
