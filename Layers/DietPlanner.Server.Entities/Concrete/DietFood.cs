using System;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class DietFood : EntityBase, IDietFood
    {
        public Guid DietId { get; set; }
        public Guid FoodId { get; set; }
        public Diet Diet { get; set; }
        public Food Food { get; set; }
    }
}
