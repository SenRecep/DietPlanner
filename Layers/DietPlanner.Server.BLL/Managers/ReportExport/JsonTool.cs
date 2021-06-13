using System;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Threading.Tasks;

using DietPlanner.DTO.FileModel;
using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.ComplexTypes;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Shared.DesignPatterns.FluentFactory;

using Microsoft.AspNetCore.Hosting;


namespace DietPlanner.Server.BLL.Managers.ReportExport
{
    public class JsonTool : IReportExport
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IGenericCommandRepository<FileModel> genericCommandRepository;
        private readonly IGenericSingleQueryRepository<FileModel> fileModelSingleQueryReposityory;
        private readonly IGenericSingleQueryRepository<Report> reportQueryRepository;
        private readonly IGenericCommandRepository<Report> reportRepository;

        public JsonTool(
            IWebHostEnvironment webHostEnvironment, 
            IGenericCommandRepository<FileModel> genericCommandRepository,
            IGenericSingleQueryRepository<FileModel> fileModelSingleQueryReposityory ,
            IGenericSingleQueryRepository<Report> reportQueryRepository,
            IGenericCommandRepository<Report> reportRepository)
        {

            this.webHostEnvironment = webHostEnvironment;
            this.genericCommandRepository = genericCommandRepository;
            this.fileModelSingleQueryReposityory = fileModelSingleQueryReposityory;
            this.reportQueryRepository = reportQueryRepository;
            this.reportRepository = reportRepository;
        }
        public ExportInfo ExportInfo { get; set; }
        public Guid ReportId { get; set; }

        public async Task<Guid> ExportAsync(SectionOrder sectionOrder)
        {
            var folderPath = $"{webHostEnvironment.WebRootPath}\\JsonReports";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var report = await reportQueryRepository.GetByIdAsync(ReportId);
            if (report is not null && report.FileModelId.HasValue)
            {
                var oldFileModel = await fileModelSingleQueryReposityory.GetByIdAsync(report.FileModelId.Value);
                if (oldFileModel is not null)
                {
                    var filePath = $"{folderPath}\\{oldFileModel.FileName}";
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                   await  genericCommandRepository.RemoveAsync(oldFileModel);
                }
            }

            var fileModel = FluentFactory<FileModel>.Init()
                .GiveAValue(x => x.CreateUserId, Guid.Parse(UserStringInfo.SystemUserId))
                .GiveAValue(x => x.CreatedTime, DateTime.Now)
                .GiveAValue(x => x.ReportId, ReportId)
                .GiveAValue(x => x.Type, Entities.Enums.FileType.JSON)
                .GiveAValue(x => x.ContentType, "application/json")
                .GiveAValue(x => x.FileName, $"{Guid.NewGuid()}.json")
                .GiveAValue(x => x.OriginalFileName, $"{ExportInfo.User.Patient.IdentityNumber}-{ExportInfo.User.Patient.FirstName}-{ExportInfo.User.Patient.LastName}.json")
                .Take();
            await genericCommandRepository.AddAsync(fileModel);
            await genericCommandRepository.Commit();

            report.FileModel = fileModel;
            await reportRepository.UpdateAsync(report);
            await reportRepository.Commit();

            var path = $"{folderPath}\\{fileModel.FileName}";
            string json = sectionOrder switch
            {
                SectionOrder.UserInfoTop => JsonSerializer.Serialize(ExportInfo),
                SectionOrder.DietInfoTop => JsonSerializer.Serialize(new
                {
                    ExportInfo.Diet,
                    ExportInfo.User
                }),
                _ => throw new NotImplementedException()
            };
            await File.WriteAllTextAsync(path, json);
            return fileModel.Id;

        }
    }
}
