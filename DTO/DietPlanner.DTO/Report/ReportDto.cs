
using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Report
{
    public class ReportDto : IDTO
    {
        public Guid ReportId { get; set; }
        public string DietName { get; set; }
        public string DiseaseName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
