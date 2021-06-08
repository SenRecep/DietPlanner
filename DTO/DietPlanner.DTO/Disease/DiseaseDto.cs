using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Disease
{
    public class DiseaseDto : IDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
