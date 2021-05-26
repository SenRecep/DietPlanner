using System;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IPerson : IEntityBase
    {
         string IdentityNumber { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
         string Email { get; set; }
         string Address { get; set; }
         string PhoneNumber { get; set; }
         string Password { get; set; }
         Guid RoleId { get; set; }
         Role Role { get; set; }
    }
}
