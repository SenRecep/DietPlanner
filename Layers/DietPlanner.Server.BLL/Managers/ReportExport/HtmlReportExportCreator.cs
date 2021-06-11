
using DietPlanner.Server.DAL.Interfaces;

namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public class HtmlReportExportCreator : ReportExportCreator
    {
        private readonly HtmlTool htmlTool;

        public HtmlReportExportCreator(IExportRepository exportRepository, HtmlTool htmlTool) : base(exportRepository)
        {
            this.htmlTool = htmlTool;
        }

        public override IReportExport ReportExportFactory()
        {
            return htmlTool;
        }
    }
}
