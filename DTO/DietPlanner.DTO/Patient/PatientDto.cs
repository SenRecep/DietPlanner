using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Patient
{
    public class PatientDto:IDTO
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
