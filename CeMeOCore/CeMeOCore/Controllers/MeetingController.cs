using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CeMeOCore.Controllers
{
    public class MeetingController : ApiController
    {
        // GET api/meeting
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/meeting/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/meeting
        public void Post([FromBody]string value)
        {
        }

        // PUT api/meeting/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/meeting/5
        public void Delete(int id)
        {
        }
    }
}