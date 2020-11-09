using FluentValidation;
using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.Filter
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InputValidationAttribute : ActionFilterAttribute
    {
        public Type ValidatorType { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var validator = (IValidator)Activator.CreateInstance(this.ValidatorType);
            var input = actionContext.ActionArguments.First().Value;            
            var results = validator.Validate(input);

            if(!results.IsValid)
            {
                for (int i = 0; i < results.Errors.Count; i++)
                {
                    actionContext.ModelState.AddModelError($"InputValidationError{i}", results.Errors[i].ErrorMessage);
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}