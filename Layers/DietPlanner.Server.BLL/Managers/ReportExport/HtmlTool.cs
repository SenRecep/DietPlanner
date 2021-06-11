using System;
using System.Threading.Tasks;

using DietPlanner.DTO.FileModel;
using DietPlanner.Server.Entities.ComplexTypes;

namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public class HtmlTool : IReportExport
    {
        public ExportInfo ExportInfo { get ; set; }

        public Task<Guid> ExportAsync(SectionOrder sectionOrder)
        {
            throw new NotImplementedException();
        }
    }
}
