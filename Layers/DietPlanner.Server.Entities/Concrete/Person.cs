using System;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Person : EntityBase, IPerson
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
