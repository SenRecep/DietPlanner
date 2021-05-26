using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Role : EntityBase, IRole
    {
        public string Name { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
