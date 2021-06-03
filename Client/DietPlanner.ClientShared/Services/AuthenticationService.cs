using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Components;

namespace DietPlanner.ClientShared.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager navigationManager;
        private readonly HttpClient httpClient;
        private readonly IUserStorageService userStorageService;

        public AuthenticationService(
            NavigationManager navigationManager,
            HttpClient httpClient,
            IUserStorageService userStorageService)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
            this.userStorageService = userStorageService;
        }
        public UserDto User { get; private set; }

        public bool IsAuthorize => User is not null;

        public async Task Initialize()
        {
            User = await userStorageService.GetAsync();
        }

        public async Task<Response<UserDto>> Login(LoginDto dto)
        {
            var httpResponse = await httpClient.PostAsJsonAsync("api/user/login", dto);
            var response = await httpResponse.Content.ReadFromJsonAsync<Response<UserDto>>();
            if (response.IsSuccessful)
            {
                User = response.Data;
                await userStorageService.SetAsync(User);
            }
            return response;
        }

        public async Task Logout()
        {
            User = null;
            await userStorageService.ClearAsync();
            navigationManager.NavigateTo("auth/login");
        }
    }
}
