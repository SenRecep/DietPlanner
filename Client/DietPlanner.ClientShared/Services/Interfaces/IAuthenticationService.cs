using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IAuthenticationService
    {
        void Initialize();
        Task<Response<UserDto>> Login(LoginDto dto);
        void Logout();
        IUserStorage UserStorage { get; }
        bool IsAuthorize { get; }
    }
}
