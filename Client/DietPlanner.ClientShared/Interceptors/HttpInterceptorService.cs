using System.Linq;
using System.Net;
using System.Net.Http.Json;

using DietPlanner.Shared.Exceptions;

using Microsoft.AspNetCore.Components;

using Toolbelt.Blazor;

namespace DietPlanner.ClientShared.Interceptors
{
    public class HttpInterceptorService
    {
        private readonly HttpStatusCode[] specificCodes = new[] {
            HttpStatusCode.InternalServerError,
            HttpStatusCode.Forbidden,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.NotFound,
        };
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
        {
            _interceptor = interceptor;
            _navManager = navManager;
        }
        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            if (specificCodes.Any(x => x.Equals(e.Response.StatusCode)))
            {
                string message;
                switch (e.Response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        _navManager.NavigateTo("/auth/accessdenied");
                        message = "You are not authorized to access here";
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/auth/unauthorized");
                        message = "User is not authorized";
                        break;
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        message = "The requested resorce was not found.";
                        break;
                    default:
                        _navManager.NavigateTo("/500");
                        message = "Something went wrong, please contact Administrator";
                        break;
                }
                throw new HttpResponseException(message);
            }
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}
