using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Containers.MicrosoftIOC;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Debugging;

namespace DietPlanner.Client
{
    public class Program
    {

        public static async Task<int> Main(string[] args)
        {
            try
            {

                Log.Logger = LoggerExtensionMethods.SerilogInit(true);

                WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

                builder.RootComponents.Add<App>("#app");

                builder.Services.AddDependencies();

                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

                Log.Information("Starting host...");

                await builder.Build().RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
