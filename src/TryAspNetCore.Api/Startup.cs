using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryAspNetCore.Api.Core.Context;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;
using TryAspNetCore.Api.Core.Repositories;
using AutoMapper;

namespace TryAspNetCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<BaseContext>(options =>
            //     options.UseNpgsql(Configuration.GetConnectionString("Default"))
            // );
            services.AddDbContext<EventContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default"))
            );

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BaseContext>()
                .AddDefaultTokenProviders();

            services.AddJWtAuthentication(Configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // We want to validate with ValidationFilter which we customize
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper();

            services.AddMvc(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.Filters.Add<ValidationFilter>();
                    options.Filters.Add<ResultWrapperFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddTransient(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            services.AddTransient(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<SerilogMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseAuthentication();
            // app.UseHttpsRedirection();

            app.UseMvc();
            // app.UseMvc(options =>
            // {
            //     options.MapRoute(
            //         name: "default",
            //         template: "api/{controller}/{id?}"
            //         );
            // });

            // app.UseMvcWithDefaultRoute();
        }
    }
}
