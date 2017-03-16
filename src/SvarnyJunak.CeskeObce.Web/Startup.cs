using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Data.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace SvarnyJunak.CeskeObce.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "MunicipalityRoute",
                  template: "{district}/{name}/{code}",
                  defaults: new { controller = "Home", action = "Index" },
                  constraints:
                      new
                      {
                          code =
                              new MunicipalityRouteConstraint(
                                  new MunicipalityCache(app.ApplicationServices.GetService<IMunicipalityRepository>()))
                      }
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public class MunicipalityRouteConstraint : IRouteConstraint
        {
            private readonly MunicipalityCache __cache;
            public MunicipalityRouteConstraint(MunicipalityCache cache)
            {
                __cache = cache;
            }

            public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
            {
                if (!values.ContainsKey(routeKey))
                    return false;

                return __cache.Contains((string)values[routeKey]);
            }
        }
    }
}
