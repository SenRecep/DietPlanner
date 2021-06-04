using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.ClientShared.StringInfo;
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
        private readonly IUserStorageSyncService userStorageService;

        public AuthenticationService(
            NavigationManager navigationManager,
            HttpClient httpClient,
            IUserStorageSyncService userStorageService,
            IUserStorage userStorage)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
            this.userStorageService = userStorageService;
            UserStorage = userStorage;
        }
        public IUserStorage UserStorage { get; }

        public bool IsAuthorize => UserStorage.User is not null;


        public void Initialize()
        {
            if (!IsAuthorize)
                UserStorage.User = userStorageService.Get();
        }

        public async Task<Response<UserDto>> Login(LoginDto dto)
        {
            var httpResponse = await httpClient.PostAsJsonAsync("api/user/login", dto);
            var response = await httpResponse.Content.ReadFromJsonAsync<Response<UserDto>>();
            if (response.IsSuccessful)
            {
                UserStorage.User = response.Data;
                userStorageService.Set(UserStorage.User);
            }
            return response;
        }

        public void Logout()
        {
            UserStorage.User = null;
            userStorageService.Clear();
            navigationManager.NavigateTo(UrlInfo.Login);
        }
    }
}
