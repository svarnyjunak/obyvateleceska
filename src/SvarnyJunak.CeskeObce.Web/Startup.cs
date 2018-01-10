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
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Joonasw.AspNetCore.SecurityHeaders;
using SvarnyJunak.CeskeObce.Web.Middlewares;
using Microsoft.ApplicationInsights.Extensibility;
using SvarnyJunak.CeskeObce.Web.Middlewares.ApplicationInsights;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

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

            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            SetupApplicationInsights();

            Configuration = builder.Build();
        }

        private void SetupApplicationInsights()
        {
            var builder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
            builder.Use((next) => new NotFoundFilter(next));
            builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddTransient<IDataLoader, JsonDataLoader>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
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
                app.UseExceptionHandler("/error");
                app.UseHttpsEnforcement();
                app.UseHsts(new HstsOptions(new TimeSpan(180, 0, 0, 0, 0), includeSubDomains: true, preload: true));
            }

            app.UseContentTypeNoSniffHeader();
            app.UseXssProtectionHeader();
            app.UseContentSecurityPolicyHeader();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/pagenotfound";
                    await next();
                }
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "Sitemap",
                  template: "sitemap.xml",
                  defaults: new { controller = "Sitemap", action = "Index" });

                routes.MapRoute(
                  name: "MunicipalityRoute",
                  template: "{district}/{name}/{code}",
                  defaults: new { controller = "Home", action = "Index" },
                  constraints:
                      new
                      {
                          code =
                              new MunicipalityRouteConstraint(
                                  new MunicipalityRepository(app.ApplicationServices.GetService<IDataLoader>()))
                      }
                );
                routes.MapRoute(
                    name: "about",
                    template: "aplikace",
                    defaults: new { controller = "Home", action = "About" });

                routes.MapRoute(
                    name: "error",
                    template: "error",
                    defaults: new { controller = "Home", action = "Error" });

                routes.MapRoute(
                    name: "pagenotfound",
                    template: "pagenotfound",
                    defaults: new { controller = "Home", action = "PageNotFound" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var supportedCultures = new[] { new CultureInfo("cs-CZ") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("cs-CZ"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }

        public class MunicipalityRouteConstraint : IRouteConstraint
        {
            private readonly MunicipalityRepository __cache;
            public MunicipalityRouteConstraint(MunicipalityRepository cache)
            {
                __cache = cache;
            }

            public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
            {
                if (!values.ContainsKey(routeKey))
                    return false;

                var query = new QueryMunicipalityByCode
                {
                    Code = (string)values[routeKey]
                };

                return __cache.Exists(query);
            }
        }
    }
}
