using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace TryAspNetCore.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" }.json", optional: true)
            .Build();
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                // .MinimumLevel.Debug()
                // .MinimumLevel.Override("System", LogEventLevel.Warning)
                // .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                // .Enrich.FromLogContext()
                // .Enrich.WithProperty("Application", "Berdan.FirstWebApi")
                // .WriteTo.File("log-.txt",
                // outputTemplate: "{Application} <{SourceContext}> {CorrelationId} {RequestId} [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception} {Host} {QueryString} {RequestHeader} {RequestBody}",
                // rollingInterval: RollingInterval.Day
                // )
                // .WriteTo.Console(
                //     outputTemplate: "{Application} <{SourceContext}> {CorrelationId} {RequestId} [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception} {Properties}"
                // )
                // .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://127.0.0.1:9200"))
                // {
                //     CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                //     TemplateName = "serilog-events-template",
                //     IndexFormat = "berdan-index-{0:yyyy.MM.dd}"
                // })
                // .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting the web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web host didn't start!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseSerilog();
    }
}
