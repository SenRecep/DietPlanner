
using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Auth
{
    public record UserDto : IDTO
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public RoleDto Role { get; set; }
    }
}
