using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.Models
{
    public class NotNullValidation : ActionFilterAttribute
    {
        public async override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestString = await actionContext.Request.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(requestString))
            {
                actionContext.ModelState.AddModelError("null", "can not be null!");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}