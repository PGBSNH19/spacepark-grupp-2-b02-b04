using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpacePark.Models;
using SpacePark.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace SpacePark
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
=======
            services.AddApplicationInsightsTelemetryWorkerService();
>>>>>>> master
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<SpaceParkContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IParkinglotRepository, ParkinglotRepository>();
            services.AddScoped<ISpaceshipRepository, SpaceshipRepository>();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddApplicationInsightsTelemetry();
            services.AddLogging(builder =>
            {
                builder.AddApplicationInsights(new AzureKeyVaultService().GetKeyVaultSecret("https://spacepark-kv-dev-01.vault.azure.net/secrets/Instrumentation--Key/ce7c7539473643648d560d8d81f0217b"));
                builder.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
                builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Error);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseMvc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
