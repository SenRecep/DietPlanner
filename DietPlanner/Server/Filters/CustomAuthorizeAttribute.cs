
using System;
using System.Linq;
using System.Net;
using System.Text.Json;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Shared.ExtensionMethods;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace DietPlanner.Server.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            allowedroles = roles;
        }

        private static void NotExistToken(AuthorizationFilterContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide token";
            filterContext.Result = Response<NoContent>.Fail(
                statusCode: (int)HttpStatusCode.ExpectationFailed,
                isShow: false,
                path: "AuthorizeAttribute",
                errors: "Please Provide token"
                ).CreateResponseInstance();
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
                return;

            StringValues authModelJson = filterContext.HttpContext.Request.Headers[AuthInfo.CUSTOM_AUTH_HEADER_KEY];
            if (authModelJson.ToString().IsEmpty())
            {
                NotExistToken(filterContext);
                return;
            }
            AuthModel authModel;
            try
            {
                authModel = JsonSerializer.Deserialize<AuthModel>(authModelJson);
            }
            catch
            {
                NotExistToken(filterContext);
                return;
            }

            if (authModel.Role.IsEmpty() || authModel.UserId.ToString().IsEmpty() || authModel.UserId.Equals(new Guid()))
            {
                NotExistToken(filterContext);
                return;
            }

            HttpResponse response = filterContext.HttpContext.Response;
            response.Headers.Add("userId", authModel.UserId.ToString());
            response.Headers.Add("role", authModel.Role);
            if (IsValidToken(authModel))
            {
                response.Headers.Add("AuthStatus", "Authorized");
                response.Headers.Add("storeAccessiblity", "Authorized");
                return;
            }
            else
            {
                response.Headers.Add("AuthStatus", "NotAuthorized");
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                filterContext.Result = Response<NoContent>.Fail(
                    statusCode: (int)HttpStatusCode.Forbidden,
                    isShow: false,
                    path: "AuthorizeAttribute",
                    errors: new[] { "Access denied", "Not Authorized" }
                ).CreateResponseInstance();
            }

        }

        public bool IsValidToken(AuthModel authModel)
        {
            if (allowedroles.Length>0)
                 return allowedroles.Any(x=>x.Equals(authModel.Role));
            return true;
        }
    }
}
