using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.ComplexTypes;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IExportRepository
    {
        Task<ExportInfo> GetExportInfoByReportIdAsync(Guid reportId);
        Task<List<ReportInfo>> GetExportInfosAsync(Guid userId);
    }
}
