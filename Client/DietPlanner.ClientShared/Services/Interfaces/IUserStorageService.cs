using System.Threading.Tasks;

using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IUserStorageService
    {
        Task<UserDto> GetAsync();
        Task SetAsync(UserDto userDto);
        Task ClearAsync();
    }
}
