
using System;
using System.Net.Http;
using System.Reflection;

using Blazored.LocalStorage;
using Blazored.SessionStorage;

using DietPlanner.ClientShared.Interceptors;
using DietPlanner.ClientShared.Services;
using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.ClientShared.Services.UserServices;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Validation;
using DietPlanner.DTO.Validation.FluentValidation.Auth;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace DietPlanner.ClientShared.Containers.MicrosoftIOC
{
    public static class MicrosoftIocExtension
    {
        private const string HTTP_CLIENT_NAME = "httpClient";
        public static void AddDependencies(this IServiceCollection services, string baseAddress)
        {
            //services.AddBlazoredSessionStorage(cnf =>
            //{
            //    cnf.JsonSerializerOptions.WriteIndented = true;
            //});

            services.AddBlazoredLocalStorage(cnf =>
            {
                cnf.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddHttpClientInterceptor();

            services.AddScoped<StorageAuthHandler>();

            services.AddHttpClient(HTTP_CLIENT_NAME, (sp, cl) =>
            {
                cl.BaseAddress = new Uri(baseAddress);
                cl.EnableIntercept(sp);
            }).AddHttpMessageHandler<StorageAuthHandler>();

            services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient(HTTP_CLIENT_NAME));

            services.AddScoped<HttpInterceptorService>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>(cnf => cnf.BaseAddress = new Uri(baseAddress));

            services.AddScoped<IAdminHttpService,AdminHttpService>();

            //services.AddScoped<IUserStorageService, UserSessionService>();
            //services.AddScoped<IUserStorageSyncService, UserSessionSyncService>();

            services.AddScoped<IUserStorageService, UserLocalStorageService>();
            services.AddScoped<IUserStorageSyncService, UserLocalStorageSyncService>();

            services.AddSingleton<IPageStateService,PageStateService>();
            services.AddSingleton<IUserStorage,UserStorage>();

            services.AddValidatorsFromAssemblyContaining<ValidationLayer>();
        }
    }
}
