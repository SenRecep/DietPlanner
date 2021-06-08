
using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Person
{
    public record UserCreateDto : IDTO
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; } = "";



        //--------------------
        public Guid RoleId { get; set; }
        public Guid CreateUserId { get; set; }
    }
}
