using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpacePark.Db_Context;
using SpacePark.Models;


namespace SpacePark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SpaceParkContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("logtest");
                try
                {
                    var initializer = new DbInitizalizer();
                    
                    initializer.Initialize(context);
                    logger.LogInformation("Context initialized.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }


            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logBuilder =>
                {
                    logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                    logBuilder.AddConsole();
                    logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
                });
    }
}
