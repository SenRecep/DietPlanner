
using Blazored.LocalStorage;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.ClientShared.StringInfo;
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.UserServices
{
    public class UserLocalStorageSyncService : IUserStorageSyncService
    {
        private readonly ISyncLocalStorageService syncLocalStorageService;
        public UserLocalStorageSyncService(ISyncLocalStorageService syncLocalStorageService) => this.syncLocalStorageService = syncLocalStorageService;

        public void Clear() => syncLocalStorageService.RemoveItem(StorageInfo.LOGIN_USER_KEY);

        public UserDto Get() => syncLocalStorageService.GetItem<UserDto>(StorageInfo.LOGIN_USER_KEY);

        public void Set(UserDto userDto) => syncLocalStorageService.SetItem(StorageInfo.LOGIN_USER_KEY, userDto);
    }
}
