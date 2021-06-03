
using Blazored.SessionStorage;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services
{
    public class UserSessionSyncService : IUserStorageSyncService
    {
        private const string SESSION_KEY = "loginUser";
        private readonly ISyncSessionStorageService sessionStorageService;

        public UserSessionSyncService(ISyncSessionStorageService sessionStorageService) => this.sessionStorageService = sessionStorageService;

        public void Clear() => sessionStorageService.RemoveItem(SESSION_KEY);

        public UserDto Get() => sessionStorageService.GetItem<UserDto>(SESSION_KEY);


        public void Set(UserDto userDto) => sessionStorageService.SetItem(SESSION_KEY, userDto);
    }
}
