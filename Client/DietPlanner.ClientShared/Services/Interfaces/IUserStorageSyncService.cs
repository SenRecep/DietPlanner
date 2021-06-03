
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IUserStorageSyncService
    {
        UserDto Get();
        void Set(UserDto userDto);
        void Clear();
    }
}
