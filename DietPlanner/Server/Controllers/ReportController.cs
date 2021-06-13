using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(RoleInfo.Dietician)]
    public class ReportController : ControllerBase
    {
        private readonly IGenericCommandService<Report> genericReportCommandService;
        private readonly IPatientRepository patientRepository;

        public ReportController(
            IGenericCommandService<Report> genericReportCommandService,
            IPatientRepository patientRepository)
        {
            this.genericReportCommandService = genericReportCommandService;
            this.patientRepository = patientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDto reportCreateDto)
        {

            FluentFactory<ReportCreateDto>.Init(reportCreateDto)
                .GiveAValue(x => x.DieticianId, Response.GetUserId())
                .GiveAValue(x => x.CreateUserId, Response.GetUserId());
            (bool State, List<string> Errors) = await patientRepository.CheckReportDateByUserIdAsync(
                reportCreateDto.PatientId,
                new() { Start = reportCreateDto.StartTime, End = reportCreateDto.EndTime });

            if (!State)
            {
                Errors.Add("Seçtiğiniz aralıkta çakışan diyetleriniz mevcut");
                return Response<Guid>.Fail(
                       statusCode: StatusCodes.Status400BadRequest,
                       isShow: true,
                       path: "[HTTPPOST] api/report",
                       errors: Errors.ToArray()
                       ).CreateResponseInstance();
            }


            var report = await genericReportCommandService.AddAsync(reportCreateDto);
            bool comitState = await genericReportCommandService.Commit();
            if (!comitState)
                return Response<Guid>.Fail(
                    statusCode: StatusCodes.Status500InternalServerError,
                    isShow: false,
                    path: "[HTTPPOST] api/report",
                    errors: "Diyet atamasi yapilirken bir hata ile karsilasildi"
                    )
                    .CreateResponseInstance();
            return Response<Guid>.Success(report.Id, StatusCodes.Status201Created).CreateResponseInstance();

        }
    }
}
