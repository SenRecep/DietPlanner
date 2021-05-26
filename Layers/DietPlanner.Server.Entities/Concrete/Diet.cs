
using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Diet : EntityBase, IDiet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Report> Reports { get; set; }
    }
}
