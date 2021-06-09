using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Food
{
    public class DietFoodCreateDto:IDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}
