using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.Controllers
{
    public sealed class NotNullValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string rawRequest;
            using (var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result))
            {
                stream.BaseStream.Position = 0;
                rawRequest = stream.ReadToEnd();
            }

            if (string.IsNullOrEmpty(rawRequest))
            {
                actionContext.ModelState.AddModelError("dummy", "request is empty!!!");
            }

            base.OnActionExecuting(actionContext);      // resume execution of the OnActionExecuting() method in the main (base) class... Like 'super.OnActionExecuting()'
        }
    }
}