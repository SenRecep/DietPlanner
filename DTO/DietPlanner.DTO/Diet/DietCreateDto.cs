using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.DTO.Food;

namespace DietPlanner.DTO.Diet
{
    public class DietCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<FoodSimpleCreateDto> DietFoods { get; set; }
        public IEnumerable<FoodCreateDto> TransferDietFoods { get; set; }
        //------------------
        public Guid CreateUserId { get; set; }
    }
}
