using System;

namespace DietPlanner.Server.Entities.ComplexTypes
{
    public record ReportInfo(Guid ReportId, string DietName, string DiseaseName, DateTime CreateTime, DateTime StartTime, DateTime EndTime);
}
