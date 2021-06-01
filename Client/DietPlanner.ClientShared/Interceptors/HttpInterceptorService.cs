using System.Net;

using DietPlanner.Shared.Exceptions;

using Microsoft.AspNetCore.Components;

using Toolbelt.Blazor;

namespace DietPlanner.ClientShared.Interceptors
{
    public class HttpInterceptorService
    {
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
            if (!e.Response.IsSuccessStatusCode)
            {
                HttpStatusCode statusCode = e.Response.StatusCode;
                string message;
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        message = "The requested resorce was not found.";
                        break;
                    case HttpStatusCode.Forbidden:
                        _navManager.NavigateTo("/403");
                        message = "You are not authorized to access here";
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/unauthorized");
                        message = "User is not authorized";
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
