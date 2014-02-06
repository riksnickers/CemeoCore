using CeMeOCore.DAL.UnitsOfWork;
using CeMeOCore.DAL.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers.Api
{
    /// <summary>
    /// This is the locationController. Here you can find the actions for Locations
    /// </summary>
    [Authorize]
    [Route("api/Location")]
    public class LocationController : ApiController
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILog logger = log4net.LogManager.GetLogger(typeof(LocationController));
        /// <summary>
        /// Unit of work private property
        /// </summary>
        private LocationUoW _locationUoW;

        /// <summary>
        /// Constructur will initialize the Unit of Work
        /// </summary>
        public LocationController()
        {
            this._locationUoW = new LocationUoW();
        }

        // GET api/Location
        /// <summary>
        /// Get one or more locations
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public IEnumerable<Location> Get()
        {
            return this._locationUoW.LocationRepository.Get();
        }

        // GET api/values/5
        /// <summary>
        /// Get a specific location
        /// </summary>
        /// <param name="mes">Request message </param>
        /// <param name="id">id of the location</param>
        /// <returns></returns>
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
        /// <summary>
        /// Insert a new Location
        /// </summary>
        /// <param name="value">Location model</param>
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
        /// <summary>
        /// Update a location
        /// </summary>
        /// <param name="value">Location Model</param>
        [AcceptVerbs("PUT")]
        public void Put([FromBody]Location value)
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
        /// <summary>
        /// Delete a Location by ID
        /// </summary>
        /// <param name="id">id needed to delete a location</param>
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

        /// <summary>
        /// Dispose the Controller + Unit of work
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (this._locationUoW != null)
            { 
                this._locationUoW.Dispose(); 
            }

            base.Dispose(disposing);
        }
    }
}
