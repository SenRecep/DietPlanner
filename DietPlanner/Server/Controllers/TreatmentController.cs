using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.Other;
using DietPlanner.DTO.Patient;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
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
    public class TreatmentController : ControllerBase
    {
        private readonly IGenericQueryService<Diet> dietService;
        private readonly IGenericQueryService<Patient> patientService;
        private readonly IGenericQueryService<Disease> diseaseService;

        public TreatmentController(
            IGenericQueryService<Diet> dietService,
             IGenericQueryService<Patient> patientService,
              IGenericQueryService<Disease> diseaseService)
        {
            this.dietService = dietService;
            this.patientService = patientService;
            this.diseaseService = diseaseService;
        }
        public IActionResult Get()
        {
            var dietsTask = dietService.GetAllAsync<DietDto>();
            var patientsTask = patientService.GetAllAsync<PatientDto>();
            var diseaseTask = diseaseService.GetAllAsync<DiseaseDto>();
            Task.WaitAll(dietsTask, patientsTask, diseaseTask);
            var treatment = FluentFactory<TreatmentDto>.Init()
                .GiveAValue(x => x.Diets, dietsTask.Result)
                .GiveAValue(x => x.Patients, patientsTask.Result)
                .GiveAValue(x => x.Diseases, diseaseTask.Result)
                .Take();
            return Response<TreatmentDto>.Success(treatment).CreateResponseInstance();
        }
    }
}
