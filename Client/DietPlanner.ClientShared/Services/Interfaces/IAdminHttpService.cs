using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IAdminHttpService
    {
        Task<Response<IEnumerable<UserDto>>> GetAllDietician();
    }
}
