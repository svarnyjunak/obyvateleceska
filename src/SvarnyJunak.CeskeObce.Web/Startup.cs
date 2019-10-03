﻿using System;
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
using Joonasw.AspNetCore.SecurityHeaders;
using Joonasw.AspNetCore.SecurityHeaders.Csp.Builder;
using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Web.Middlewares;
using Microsoft.Extensions.Hosting;
using SvarnyJunak.CeskeObce.Web.Middlewares.ApplicationInsights;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddApplicationInsightsTelemetryProcessor<NotFoundFilter>();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.ConstraintMap.Add("municipalityCode", typeof(MunicipalityRouteConstraint));
            });
            
            services
                .AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IDataLoader, JsonDataLoader>();
            services.AddTransient<MunicipalityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");

                //todo: use app.UseHttpsRedirection();
                app.UseHttpsEnforcement();
                app.UseHsts(new HstsOptions(new TimeSpan(180, 0, 0, 0, 0), includeSubDomains: true, preload: true));
            }

            //todo: app.UseCors();
            app.UseContentTypeNoSniffHeader();
            
            //app.UseXssProtectionHeader();
            app.UseXXssProtection(new XXssProtectionOptions(true, true));

            // todo: app.UseCsp(...)
            app.UseContentSecurityPolicyHeader();

            // todo: use app.UseStatusCodePagesWithRedirects(...)
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
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
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
