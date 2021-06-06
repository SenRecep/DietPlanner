using System;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.Server.Entities.Enums;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<UserDto> LoginAsync(LoginDto dto);
        Task<UserDto> GetPersonByIdentityNumber(PersonType personType, string identityNumber);
    }
}
