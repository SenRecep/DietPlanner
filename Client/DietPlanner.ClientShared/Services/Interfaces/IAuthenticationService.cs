using System.Threading.Tasks;

using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserDto User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
        bool IsAuthorize { get; }
    }
}
