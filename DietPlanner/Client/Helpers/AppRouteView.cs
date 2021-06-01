using System;
using System.Linq;
using System.Net;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DietPlanner.Client.Helpers
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            AuthorizeAttribute authorizeAttribute = null;
            var attribute = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute));
            bool authorize = attribute != null;
            if (authorize)
                authorizeAttribute = (AuthorizeAttribute)attribute;

            if (authorize && AuthenticationService.User == null)
            {
                string returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
                NavigationManager.NavigateTo($"auth/login/{returnUrl}");
                return;
            }

            if (authorize && authorizeAttribute != null && !authorizeAttribute.Roles.IsEmpty())
            {
                if (AuthenticationService.User.Role != null && !AuthenticationService.User.Role.Name.IsEmpty())
                {
                    var access = authorizeAttribute.Roles.Split(",").Any(x => x.Equals(AuthenticationService.User.Role.Name));

                }
            }
            base.Render(builder);
        }
    }
}
