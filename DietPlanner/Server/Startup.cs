
using System;
using System.IO;
using System.Reflection;

using DietPlanner.Server.BLL.Containers.MicrosoftIOC;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.Filters;
using DietPlanner.Server.Seed;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DietPlanner.Server
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies(configuration, environment);
            services.AddScoped<Seeder>();
            services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidateModelAttribute>();
            }).AddValidationDependencies();
            services.AddCustomValidationResponse();
            services.AddRazorPages();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DietPlanner.WebApi",
                    Version = "v1",
                    Description = "DietPlanner WebApi",
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/SenRecep/GeneralStockMarketSystem/blob/master/LICENSE")
                    }

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DietPlanner.Server v1"));

            app.UseCustomExceptionHandler();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
