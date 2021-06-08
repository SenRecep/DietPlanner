using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Diet
{
    public class DietDto : IDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
