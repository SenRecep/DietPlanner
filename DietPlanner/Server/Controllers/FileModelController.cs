using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.FileModel;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Managers.ReportExport;
using DietPlanner.Server.Entities.ComplexTypes;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DietPlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthorize(RoleInfo.Dietician)]
    public class FileModelController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;

        public FileModelController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FileModelCreateDto fileModelCreateDto)
        {
            ReportExportCreator reportExportCreator = fileModelCreateDto.FileType switch
            {
                FileType.HTML => serviceProvider.GetService<HtmlReportExportCreator>(),
                FileType.JSON => serviceProvider.GetService<JsonReportExportCreator>(),
                _ => throw new NotImplementedException()
            };
            IReportExport reportExport = reportExportCreator.ReportExportFactory();
            reportExport.ExportInfo = await reportExportCreator.GetExportInfoAsync(fileModelCreateDto.ReportId);
            var fileModelId= await reportExport.ExportAsync(fileModelCreateDto.SectionOrder);

            return Response<ExportInfo>.Success(reportExport.ExportInfo, StatusCodes.Status200OK).CreateResponseInstance();
        }
    }
}
