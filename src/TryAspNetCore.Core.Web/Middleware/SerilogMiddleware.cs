using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace TryAspNetCore.Core.Web
{
    public class SerilogMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public SerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //TODO: if something goes wrong, what happens in here?
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<SerilogMiddleware>>();

            httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationIdValue);
            string correlationId = correlationIdValue.Count == 0 || string.IsNullOrWhiteSpace(correlationIdValue[0]) ? Guid.NewGuid().ToString() : correlationIdValue[0];
            //We can also change the requestid, ["RequestId"] = correlationId 
            using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
            {
                logger.LogInformation("Begin the request");
                //TODO: use options for log to request
                if (true)
                {
                    //We can log to all request information for the test case, header, body, form(?)
                    //It will be security problem for the authentication, we have to skip that request
                    LogForRequest(httpContext);
                }

                httpContext.TraceIdentifier = correlationId;
                await _next(httpContext);
                logger.LogInformation("End the request");
            }
        }

        private async void LogForRequest(HttpContext httpContext)
        {
            var body = await FormatRequest(httpContext.Request);
            var loggerMiddleware = Serilog.Log.ForContext<SerilogMiddleware>();
            loggerMiddleware = loggerMiddleware
                .ForContext("Host", httpContext.Request.Host)
                .ForContext("QueryString", httpContext.Request.QueryString)
                .ForContext("RequestHeader", httpContext.Request.Headers.Where(h => h.Key != "Authorization").ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestBody", body.Replace("\n", "").Replace("\r", "").Replace("\t", ""));
            loggerMiddleware.Write(LogEventLevel.Information, "Request information");
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            request.Body.Position = 0;

            return Encoding.UTF8.GetString(buffer);
        }

        // public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        // {
        //     _contextAccessor.HttpContext.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId);
        //     logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", (object)correlationId ?? Guid.NewGuid()));
        // }
    }
}