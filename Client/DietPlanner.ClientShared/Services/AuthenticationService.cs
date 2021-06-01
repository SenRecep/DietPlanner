using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
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

        public async Task Login(string username, string password)
        {
            //Istek atilacak
            User = new UserDto()
            {
                Address = "Address",
                Email = "mail",
                FirstName = "recep",
                IdentityNumber = "77",
                LastName = "sen",
                PhoneNumber = "num",
                Role = new RoleDto()
                {
                    Name = "admin"
                },
                Id = Guid.NewGuid()
            };
            await userStorageService.SetAsync(User);
        }

        public async Task Logout()
        {
            User = null;
            await userStorageService.ClearAsync();
            navigationManager.NavigateTo("auth/login");
        }
    }
}
