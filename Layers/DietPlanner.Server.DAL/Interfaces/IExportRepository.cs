using System;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.ComplexTypes;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IExportRepository
    {
        Task<ExportInfo> GetExportInfoByReportIdAsync(Guid reportId);
    }
}
