using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TryAspNetCore.Core.Web
{
    //Handle the exception and return formatted result
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return;

            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = new ResponseResult()
            {
                Success = false,
                Status = statusCode,
                Error = new ErrorInformation(),
            };
            if (context.Exception is BaseException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                result.Error.Title = context.Exception.Message ?? "Request Validation Error";
                foreach (var validationError in (context.Exception as CustomValidationException).ValidationErrors)
                {
                    result.Error.Messages.Add(validationError.ErrorMessage);
                }
            }
            else
            {
                result.Error.Title = "An unexpected error occurred!";
                //TODO: we have to provide for more information if the client is trusted
                //, we can check the enviroment variable or configuration and add the stacktrace information
                result.Error.Messages.Add(context.Exception.Message);

                //Internal error, we have to add error to log system
                _logger.LogError(context.Exception, "Internal server error");
            }

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(result);

            //We handled the exception
            context.Exception = null;
        }
    }
}