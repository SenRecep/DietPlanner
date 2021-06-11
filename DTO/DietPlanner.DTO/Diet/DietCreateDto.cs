using System;
using System.Collections.Generic;

using DietPlanner.DTO.Food;
using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Diet
{
    public class DietCreateDto : IDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //------------------
        public IEnumerable<FoodSimpleCreateDto> SimpleDietFoods { get; set; }
        public Guid CreateUserId { get; set; }
    }
}
