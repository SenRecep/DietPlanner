using System.Collections.Generic;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IDiet : IEntityBase, IReportList
    {
        string Name { get; set; }
        string Description { get; set; }
        public IEnumerable<DietFood> DietFoods { get; set; }
    }
}
