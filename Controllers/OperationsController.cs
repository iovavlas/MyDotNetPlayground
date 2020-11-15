using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class OperationsController : ApiController
    {
        [HttpOptions]
        [Route("api/operation1")]
        public IHttpActionResult DoSomeShitOnTheServer()
        {
            try
            {
                // operate a function on the server - RPC (Remote Procedure Call) like, without returning any data to the client...
                return Ok();    
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
