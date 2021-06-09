using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Report
{
    public class ReportCreateDto : IDTO
    {
        public Guid PatientId { get; set; }
        public Guid DieticianId { get; set; }
        public Guid DiseaseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        //-------------------
        public Guid DietId { get; set; }
        public Guid CreateUserId { get; set; }
    }
}
