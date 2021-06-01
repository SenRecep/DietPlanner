using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.Shared.ExtensionMethods;
using DietPlanner.Shared.StringInfo;

namespace DietPlanner.ClientShared.Interceptors
{
    public class SessionAuthHandler : DelegatingHandler
    {
        private readonly IUserStorageService userSessionService;

        public SessionAuthHandler(IUserStorageService userSessionService)
        {
            this.userSessionService = userSessionService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            UserDto loginUser = await userSessionService.GetAsync();
            if (loginUser.IsNull())
                return new(HttpStatusCode.Unauthorized);
            AuthModel authModel = new(loginUser.Id, loginUser.Role?.Name);
            string authJson = JsonSerializer.Serialize(authModel);
            request.Headers.Add(AuthInfo.CUSTOM_AUTH_HEADER_KEY, authJson);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return new(HttpStatusCode.Forbidden);
            return response;
        }
    }
}
