using System.Collections.Generic;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IRole : IEntityBase
    {
        string Name { get; set; }
        IEnumerable<Admin> Admins { get; set; }
        IEnumerable<Patient> Patients { get; set; }
        IEnumerable<Dietician> Dieticians { get; set; }
    }
}
