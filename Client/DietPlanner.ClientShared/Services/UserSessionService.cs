using System.Threading.Tasks;

using Blazored.SessionStorage;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services
{
    public class UserSessionService : IUserStorageService
    {
        private const string SESSION_KEY = "loginUser";
        private readonly ISessionStorageService sessionStorageService;

        public UserSessionService(ISessionStorageService sessionStorageService) => this.sessionStorageService = sessionStorageService;
        public async Task ClearAsync() => await sessionStorageService.RemoveItemAsync(SESSION_KEY);

        public async Task<UserDto> GetAsync() => await sessionStorageService.GetItemAsync<UserDto>(SESSION_KEY);

        public async Task SetAsync(UserDto userDto) => await sessionStorageService.SetItemAsync(SESSION_KEY, userDto);
    }
}
