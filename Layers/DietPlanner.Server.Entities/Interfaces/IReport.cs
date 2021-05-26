using System;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IReport : IEntityBase
    {
        Patient Patient { get; set; }
        Dietician Dietician { get; set; }
        Disease Disease { get; set; }
        Diet Diet { get; set; }


        Guid PatientId { get; set; }
        Guid DieticianId { get; set; }
        Guid DiseaseId { get; set; }
        Guid DietId { get; set; }
    }
}
