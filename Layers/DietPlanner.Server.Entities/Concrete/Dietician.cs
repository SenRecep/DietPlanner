using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Dietician : Person, IDietician
    {
        public IEnumerable<Report> Reports { get; set; }
    }
}
