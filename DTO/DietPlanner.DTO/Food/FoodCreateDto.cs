using System;

namespace DietPlanner.DTO.Food
{
    public class FoodCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}
