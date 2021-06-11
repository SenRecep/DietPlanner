using System;
using System.Threading.Tasks;

using DietPlanner.DTO.FileModel;
using DietPlanner.Server.Entities.ComplexTypes;

namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public interface IReportExport
    {
        public ExportInfo ExportInfo { get; set; }
        Task<Guid> ExportAsync(SectionOrder sectionOrder);
    }
}
