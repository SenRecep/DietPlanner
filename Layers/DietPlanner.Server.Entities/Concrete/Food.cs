using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Food : EntityBase, IFood
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DietFood> DietFoods { get; set; }
    }
}
