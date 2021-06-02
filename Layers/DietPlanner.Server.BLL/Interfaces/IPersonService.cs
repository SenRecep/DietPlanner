using System.Threading.Tasks;

using DietPlanner.DTO.Auth;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<UserDto> LoginAsync(LoginDto dto);
    }
}
