
using System;
using System.Net.Http;

using Blazored.SessionStorage;

using DietPlanner.ClientShared.Interceptors;
using DietPlanner.ClientShared.Services;
using DietPlanner.ClientShared.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace DietPlanner.ClientShared.Containers.MicrosoftIOC
{
    public static class MicrosoftIocExtension
    {
        private const string HTTP_CLIENT_NAME = "httpClient";
        public static void AddDependencies(this IServiceCollection services, string baseAddress)
        {

            services.AddBlazoredSessionStorage(cnf =>
            {
                cnf.JsonSerializerOptions.WriteIndented = true;
            });


            services.AddScoped<StorageAuthHandler>();

            services.AddHttpClient(HTTP_CLIENT_NAME, (sp, cl) =>
            {
                cl.BaseAddress = new Uri(baseAddress);
                cl.EnableIntercept(sp);
            }).AddHttpMessageHandler<StorageAuthHandler>();

            services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient(HTTP_CLIENT_NAME));
            services.AddHttpClientInterceptor();

            services.AddScoped<HttpInterceptorService>();

            services.AddHttpClient<ITestService, TestService>(cnf=> cnf.BaseAddress= new Uri(baseAddress));

            services.AddHttpClient<IAuthenticationService, AuthenticationService>(cnf => cnf.BaseAddress = new Uri(baseAddress));

            services.AddScoped<IUserStorageService, UserSessionService>();


        }
    }
}
