
using DietPlanner.Server.DAL.Interfaces;

namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public class JsonReportExportCreator : ReportExportCreator
    {
        private readonly JsonTool jsonTool;

        public JsonReportExportCreator(IExportRepository exportRepository, JsonTool jsonTool) : base(exportRepository)
        {
            this.jsonTool = jsonTool;
        }

        public override IReportExport ReportExportFactory()
        {
            return jsonTool;
        }
    }
}
