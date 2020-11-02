using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.Controllers
{
    public sealed class NotNullValidation : ActionFilterAttribute
    {
        public async override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestString = await actionContext.Request.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(requestString))    // TODO: returns always true...?
            {
                actionContext.ModelState.AddModelError("dummy", "request is empty!!!");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}