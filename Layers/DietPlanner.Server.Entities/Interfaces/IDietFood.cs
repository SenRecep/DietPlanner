using System;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IDietFood : IEntityBase
    {
        public Guid DietId { get; set; }
        public Guid FoodId { get; set; }

        public Diet Diet { get; set; }
        public Food Food { get; set; }
    }
}
