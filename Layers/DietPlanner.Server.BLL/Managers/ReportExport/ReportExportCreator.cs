using System;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.ComplexTypes;

namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public abstract class ReportExportCreator
    {
        private readonly IExportRepository exportRepository;

        public ReportExportCreator(IExportRepository exportRepository)
        {
            this.exportRepository = exportRepository;
        }
        public abstract IReportExport ReportExportFactory();

        public virtual Task<ExportInfo> GetExportInfoAsync(Guid reportId)
        {
            return exportRepository.GetExportInfoByReportIdAsync(reportId);
        }
    }
}
