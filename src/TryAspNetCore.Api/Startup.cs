using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSwag.AspNetCore;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Http;
using TryAspNetCore.Core;
using TryAspNetCore.Core.Web;
using TryAspNetCore.IdentityManagement;
using TryAspNetCore.EventManagement;
using TryAspNetCore.EntityFrameworkCore.Repository;
using TryAspNetCore.Api.Core;

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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default"), npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("IdentitiyContext");
                })
            );
            services.AddDbContext<EventContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseNpgsql(Configuration.GetConnectionString("Default"), npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsAssembly("EventContext");
                    })
            );

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddJWtAuthentication(Configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // We want to validate with ValidationFilter which we customize
                options.SuppressModelStateInvalidFilter = true;
            });

            // services.AddAutoMapper();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
                // cfg.AllowNullCollections = true;
                // cfg.AllowNullDestinationValues = true;
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.Filters.Add<ValidationFilter>();
                    options.Filters.Add<ResultWrapperFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddTransient(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            services.AddTransient(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));

            services.AddSingleton<IAmbientDataContext, AmbientDataContext>();
            services.AddSingleton<ISessionManager, SessionManager>();

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<SerilogMiddleware>();

            //TODO: It's not a good solution for migrate the db
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EventContext>())
                {
                    context.Database.Migrate();
                }
            }

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
            app.UseMiddleware<DefaultSessionMiddleware>();
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
