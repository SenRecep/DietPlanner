using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Role : EntityBase, IRole
    {
        public string Name { get; set; }
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Dietician> Dieticians { get; set; }
    }
}
