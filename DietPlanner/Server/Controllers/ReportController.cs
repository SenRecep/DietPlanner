using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.ControllerBases;
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
    public class ReportController : CustomControllerBase
    {
        private readonly IGenericCommandService<Report> genericReportCommandService;

        public ReportController(IGenericCommandService<Report> genericReportCommandService)
        {
            this.genericReportCommandService = genericReportCommandService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDto reportCreateDto)
        {
            FluentFactory<ReportCreateDto>.Init(reportCreateDto)
                .GiveAValue(x => x.DieticianId, Response.GetUserId())
                .GiveAValue(x => x.CreateUserId, Response.GetUserId())
                .GiveAValue(x => x.EndTime, DateTime.Now);
            await genericReportCommandService.AddAsync(reportCreateDto);
            var comitState = await genericReportCommandService.Commit();
            if (!comitState)
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status500InternalServerError,
                    isShow: false,
                    path: "[HTTPPOST] api/report",
                    errors: "Diyet atamasi yapilirken bir hata ile karsilasildi"
                    )
                    .CreateResponseInstance();
            return Response<NoContent>.Success(StatusCodes.Status201Created).CreateResponseInstance();

        }
    }
}
