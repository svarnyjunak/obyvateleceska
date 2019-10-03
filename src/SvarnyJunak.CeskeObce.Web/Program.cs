using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SvarnyJunak.CeskeObce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IHost BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(c => c.UseStartup<Startup>())
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddApplicationInsights();
                })
                .ConfigureAppConfiguration((host, configuration) =>
                {
                    var env = host.HostingEnvironment;

                    configuration
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();

                    if (env.IsDevelopment())
                    {
                        configuration.AddApplicationInsightsSettings(developerMode: true);
                    }

                    configuration.Build();
                })
                .Build();
    }
}
