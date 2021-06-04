using System.Threading.Tasks;

using Blazored.LocalStorage;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.ClientShared.StringInfo;
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services
{
    public class UserLocalStorageService : IUserStorageService
    {
        private readonly ILocalStorageService localStorageService;
        public UserLocalStorageService(ILocalStorageService localStorageService) => this.localStorageService = localStorageService;
     
        public async Task ClearAsync() => await localStorageService.RemoveItemAsync(StorageInfo.LOGIN_USER_KEY);

        public async Task<UserDto> GetAsync() => await localStorageService.GetItemAsync<UserDto>(StorageInfo.LOGIN_USER_KEY);

        public async Task SetAsync(UserDto userDto) => await localStorageService.SetItemAsync(StorageInfo.LOGIN_USER_KEY, userDto);
    }
}
