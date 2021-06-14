using System;
using System.IO;
using System.Threading.Tasks;

using DietPlanner.DTO.FileModel;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.BLL.Managers.ReportExport;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.ComplexTypes;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.ExtensionMethods;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using E_FileType = DietPlanner.Server.Entities.Enums.FileType;

namespace DietPlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileModelController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IGenericSingleQueryRepository<FileModel> fileModelService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileModelController(IServiceProvider serviceProvider, IGenericSingleQueryRepository<FileModel> fileModelService, IWebHostEnvironment webHostEnvironment)
        {
            this.serviceProvider = serviceProvider;
            this.fileModelService = fileModelService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [CustomAuthorize(RoleInfo.Dietician,RoleInfo.Patient)]
        public async Task<IActionResult> Create([FromBody] FileModelCreateDto fileModelCreateDto)
        {
            ReportExportCreator reportExportCreator = fileModelCreateDto.FileType switch
            {
                FileType.HTML => serviceProvider.GetService<HtmlReportExportCreator>(),
                FileType.JSON => serviceProvider.GetService<JsonReportExportCreator>(),
                _ => throw new NotImplementedException()
            };
            IReportExport reportExport = reportExportCreator.ReportExportFactory();
            reportExport.ReportId = fileModelCreateDto.ReportId;
            reportExport.ExportInfo = await reportExportCreator.GetExportInfoAsync(fileModelCreateDto.ReportId);
            if (reportExport.ExportInfo is null)
                return Response<Guid>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "[HTTPPOST] /api/filemodel",
                    errors: "Ilgili rapor bulunamadı"
                    ).CreateResponseInstance();
            Guid fileModelId = await reportExport.ExportAsync(fileModelCreateDto.SectionOrder);

            return Response<Guid>.Success(fileModelId, StatusCodes.Status200OK).CreateResponseInstance();
        }



        [HttpGet("{fileModelId:guid}")]
        public async Task<IActionResult> DownLoad(Guid fileModelId)
        {
            var fileModel = await fileModelService.GetByIdAsync(fileModelId);
            var folder = fileModel.Type switch
            {
                E_FileType.HTML => "HtmlReports",
                E_FileType.JSON => "JsonReports",
                _ => throw new NotImplementedException()
            };
            var folderPath = $"{webHostEnvironment.WebRootPath}\\{folder}";
            var filePath = $"{folderPath}\\{fileModel.FileName}";
            var stream = System.IO.File.OpenRead(filePath);
            var contentType = fileModel.ContentType;
            if (contentType.IsEmpty())
                contentType = "application/octet-stream";
            return File(stream, contentType, fileModel.OriginalFileName);
        }
    }
}
