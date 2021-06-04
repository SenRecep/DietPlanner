
using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;

namespace DietPlanner.ClientShared.Services.UserServices
{
    public class UserStorage : IUserStorage
    {
        public UserDto User { get; set; }
    }
}
