using System.Collections.Generic;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IFood : IEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DietFood> DietFoods { get; set; }
    }
}
