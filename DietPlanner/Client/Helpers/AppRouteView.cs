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
            Attribute attribute = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute));
            if (attribute != null)
            {
                if (AuthenticationService.User == null)
                {
                    string returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
                    NavigationManager.NavigateTo($"auth/login/{returnUrl}");
                    return;
                }

                AuthorizeAttribute authorizeAttribute = (AuthorizeAttribute)attribute;
                bool doYouHaveAccess = false;
                if (authorizeAttribute != null &&
                    !authorizeAttribute.Roles.IsEmpty() &&
                    AuthenticationService.User.Role != null &&
                    !AuthenticationService.User.Role.Name.IsEmpty())
                    doYouHaveAccess = authorizeAttribute.Roles.Split(",").Any(x => x.Equals(AuthenticationService.User.Role.Name));

                if (!doYouHaveAccess)
                {
                    string returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
                    NavigationManager.NavigateTo($"auth/accessdenied/{returnUrl}");
                    return;
                }
            }

            base.Render(builder);
        }
    }
}
