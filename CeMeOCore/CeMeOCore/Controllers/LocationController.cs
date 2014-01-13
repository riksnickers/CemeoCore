using CeMeOCore.Models;
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
        private CeMeoContext _db = new CeMeoContext();
        // GET api/Location
        [AcceptVerbs("GET")]
        public IEnumerable<Location> Get()
        {
            return _db.Locations;
        }

        // GET api/values/5
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get(HttpRequestMessage mes, int id)
        {
            Location ll;
            try
            {
                ll = _db.Locations.Where(l => l.LocationID == id).First();
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
        }

        // PUT api/values/5
        [AcceptVerbs("PUT")]
        public void Put(int id, [FromBody]Location value)
        {
        }

        // DELETE api/values/5
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            
        }
    }
}
