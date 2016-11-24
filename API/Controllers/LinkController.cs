using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Facades;

namespace API.Controllers
{
    public class LinkController : ApiController
    {
        public RouteFacade RouteFacade { get; set; }

        //GET: api/Link
        public IHttpActionResult Get(string town)
        {
            return Content(HttpStatusCode.OK, RouteFacade.GetAllStationsByTown(town));
        }

        // GET: api/Link/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Link
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Link/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Link/5
        public void Delete(int id)
        {
        }
    }
}
