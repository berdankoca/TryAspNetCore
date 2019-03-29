using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TryAspNetCore.Core.Web
{
    public class ResultWrapperFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return;

            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                if (!(result.Value is ResponseResult))
                {
                    result.Value = new ResponseResult()
                    {
                        Success = true,
                        Status = result.StatusCode ?? (int)HttpStatusCode.OK,
                        Result = result.Value
                    };
                }
            }
            else if (context.Result is JsonResult)
            {
                var result = context.Result as JsonResult;
                if (!(result.Value is ResponseResult))
                {
                    result.Value = new ResponseResult()
                    {
                        Success = true,
                        Status = result.StatusCode ?? (int)HttpStatusCode.OK,
                        Result = result.Value
                    };
                }
            }

        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}