
using System;
using System.Net.Http;

using DietPlanner.ClientShared.Interceptors;
using DietPlanner.ClientShared.Services;
using DietPlanner.ClientShared.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace DietPlanner.ClientShared.Containers.MicrosoftIOC
{
    public static class MicrosoftIocExtension
    {
        public static void AddDependencies(this IServiceCollection services, string baseAddress)
        {
            services.AddScoped<ITestService, TestService>();

            services.AddHttpClient("localHttpClient", (sp, cl) =>
            {
                cl.BaseAddress = new Uri(baseAddress);
                cl.EnableIntercept(sp);
            });
            services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("localHttpClient"));
            services.AddHttpClientInterceptor();

            services.AddScoped<HttpInterceptorService>();

        }
    }
}
