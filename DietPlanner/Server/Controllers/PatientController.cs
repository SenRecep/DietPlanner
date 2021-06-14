using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.ExtensionMethods;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(RoleInfo.Patient,RoleInfo.Dietician)]
    public class PatientController : ControllerBase
    {
        private readonly IGenericSingleQueryService<Patient> patientSingleQueryService;
        private readonly IExportRepository exportRepository;
        private readonly IMapper mapper;

        public PatientController(IGenericSingleQueryService<Patient> patientSingleQueryService,IExportRepository exportRepository,IMapper mapper)
        {
            this.patientSingleQueryService = patientSingleQueryService;
            this.exportRepository = exportRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await patientSingleQueryService.GetByIdAsync<UserDto>(id);
            if (patient.IsNull())
                return Response<UserDto>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "[HTTPGET] /api/patient/{id:guid}",
                    errors: "Aradığınız hasta mevcut değil")
                    .CreateResponseInstance();
            return Response<UserDto>.Success(patient).CreateResponseInstance();
        }
        [HttpGet("reports/{userId:guid}")]
        public async Task<IActionResult> GetPatientReports(Guid userId)
        {
            var reports = await exportRepository.GetExportInfosAsync(userId);
            var result = mapper.Map<List<ReportDto>>(reports);
            return Response<List<ReportDto>>.Success(result).CreateResponseInstance();
        }
    }
}
