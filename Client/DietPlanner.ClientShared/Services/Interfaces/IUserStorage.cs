using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IUserStorage
    {
         UserDto User { get; set; }
    }
}
