using System.ComponentModel.DataAnnotations;
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
                var exception = new CustomValidationException("Model properties is not valid!");
                foreach (var state in context.ModelState)
                {
                    foreach (var error in state.Value.Errors)
                        exception.ValidationErrors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
                throw exception;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}