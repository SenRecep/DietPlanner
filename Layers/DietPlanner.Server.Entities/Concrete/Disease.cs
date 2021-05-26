
using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Disease : EntityBase, IDisease
    {
        public string Name { get; set; }
        public IEnumerable<Report> Reports { get; set; }
    }
}
