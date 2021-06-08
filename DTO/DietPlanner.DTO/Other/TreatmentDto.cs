using System.Collections.Generic;

using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.Interfaces;
using DietPlanner.DTO.Patient;

namespace DietPlanner.DTO.Other
{
    public class TreatmentDto : IDTO
    {
        public List<PatientDto> Patients { get; set; } = new ();
        public List<DiseaseDto> Diseases { get; set; } = new();
        public List<DietDto> Diets { get; set; } = new();
    }
}
