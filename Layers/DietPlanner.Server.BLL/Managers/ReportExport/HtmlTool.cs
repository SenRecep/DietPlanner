using System;
using System.IO;
using System.Linq;
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
    public class HtmlTool : IReportExport
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IGenericCommandRepository<FileModel> genericCommandRepository;
        private readonly IGenericSingleQueryRepository<FileModel> fileModelSingleQueryReposityory;
        private readonly IGenericSingleQueryRepository<Report> reportQueryRepository;
        private readonly IGenericCommandRepository<Report> reportRepository;

        public HtmlTool(
            IWebHostEnvironment webHostEnvironment,
            IGenericCommandRepository<FileModel> genericCommandRepository,
            IGenericSingleQueryRepository<FileModel> fileModelSingleQueryReposityory,
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
            var folderPath = $"{webHostEnvironment.WebRootPath}\\HtmlReports";
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
                    await genericCommandRepository.RemoveAsync(oldFileModel);
                }
            }
            var fileModel = FluentFactory<FileModel>.Init()
                .GiveAValue(x => x.CreateUserId, Guid.Parse(UserStringInfo.SystemUserId))
                .GiveAValue(x => x.CreatedTime, DateTime.Now)
                .GiveAValue(x => x.ReportId, ReportId)
                .GiveAValue(x => x.Type, Entities.Enums.FileType.HTML)
                .GiveAValue(x => x.ContentType, "text/html")
                .GiveAValue(x => x.FileName, $"{Guid.NewGuid()}.html")
                .GiveAValue(x => x.OriginalFileName, $"{ExportInfo.User.Patient.IdentityNumber}-{ExportInfo.User.Patient.FirstName}-{ExportInfo.User.Patient.LastName}.html")
                .Take();
            await genericCommandRepository.AddAsync(fileModel);
            await genericCommandRepository.Commit();

            report.FileModel = fileModel;
            await reportRepository.UpdateAsync(report);
            await reportRepository.Commit();

            var path = $"{folderPath}\\{fileModel.FileName}";
            string templateFile = Directory.GetCurrentDirectory() + "\\Templates\\Report.html";
            var htmlContent = await File.ReadAllTextAsync(templateFile);

            htmlContent = htmlContent
                .Replace("isSwap", sectionOrder == SectionOrder.UserInfoTop ? "false" : "true")
                .Replace("[Patient.FirstName]", ExportInfo.User.Patient.FirstName)
                .Replace("[Patient.LastName]", ExportInfo.User.Patient.LastName)
                .Replace("[Patient.IdentityNumber]", ExportInfo.User.Patient.IdentityNumber)
                .Replace("[Patient.Email]", ExportInfo.User.Patient.Email)
                .Replace("[Patient.PhoneNumber]", ExportInfo.User.Patient.PhoneNumber)
                .Replace("[Patient.Address]", ExportInfo.User.Patient.Address)
                .Replace("[Dietician.FirstName]", ExportInfo.User.Dietician.FirstName)
                .Replace("[Dietician.LastName]", ExportInfo.User.Dietician.LastName)
                .Replace("[Dietician.Email]", ExportInfo.User.Dietician.Email)
                .Replace("[Dietician.PhoneNumber]", ExportInfo.User.Dietician.PhoneNumber)
                .Replace("[Disease.Name]", ExportInfo.User.Disease.Name)
                .Replace("[Diet.Name]", ExportInfo.Diet.Name)
                .Replace("[Diet.Description]", ExportInfo.Diet.Description)
                .Replace("[Diet.StartTime]", ExportInfo.Diet.StartTime.ToShortDateString())
                .Replace("[Diet.EndTime]", ExportInfo.Diet.EndTime.ToShortDateString())
                .Replace("[Diet.CreateTime]", ExportInfo.Diet.CreateTime.ToShortDateString())
                .Replace("[FOODS]", string.Join("", ExportInfo.Diet.Foods.Select(x => $"<tr><th>{x.Name}</th><th>{x.Description}</th></tr>")));
            await File.WriteAllTextAsync(path, htmlContent);
            return fileModel.Id;
        }
    }
}
