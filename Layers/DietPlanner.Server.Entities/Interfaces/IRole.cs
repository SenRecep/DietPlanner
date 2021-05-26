using System.Collections.Generic;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IRole : IEntityBase
    {
        string Name { get; set; }
        IEnumerable<Person> People { get; set; }
    }
}
