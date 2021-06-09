using System;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Report : EntityBase, IReport
    {
        public Patient Patient { get; set; }
        public Dietician Dietician { get; set; }
        public Disease Disease { get; set; }
        public Diet Diet { get; set; }
        public Guid PatientId { get; set; }
        public Guid DieticianId { get; set; }
        public Guid DiseaseId { get; set; }
        public Guid DietId { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}
