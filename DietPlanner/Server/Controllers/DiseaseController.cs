using System.Collections.Generic;
using System.Threading.Tasks;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(RoleInfo.Dietician)]
    public class DiseaseController : ControllerBase
    {
        private readonly IGenericCommandService<Disease> diseaseCommandService;
        private readonly IGenericQueryService<Disease> diseaseQueryService;

        public DiseaseController(IGenericCommandService<Disease> diseaseCommandService,IGenericQueryService<Disease> diseaseQueryService)
        {
            this.diseaseCommandService = diseaseCommandService;
            this.diseaseQueryService = diseaseQueryService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DiseaseCreateDto diseaseCreateDto)
        {
            await diseaseCommandService.AddAsync(diseaseCreateDto);
            await diseaseCommandService.Commit();
            return Response<NoContent>.Success(StatusCodes.Status201Created).CreateResponseInstance();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diseases = await diseaseQueryService.GetAllAsync<DiseaseDto>();
            return Response<IEnumerable<DiseaseDto>>.Success(diseases).CreateResponseInstance();
        }
    }
}