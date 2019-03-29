using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TryAspNetCore.Core.Web
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var exception = new CustomValidationException();
                foreach (var state in context.ModelState)
                    exception.ValidationErrors.Add(state.Key, state.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                throw exception;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}