using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
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
    [CustomAuthorize(RoleInfo.Dietician)]
    public class DietController : ControllerBase
    {
        private readonly IGenericCommandRepository<Diet> dietCommandService;

        public DietController(IGenericCommandRepository<Diet> dietCommandService)
        {
            this.dietCommandService = dietCommandService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DietCreateDto dietCreateDto)
        {
            if (dietCreateDto.DietFoods.IsNull() || (dietCreateDto.DietFoods.IsNotNull() && !dietCreateDto.DietFoods.Any()))
            {
                return Response<NoContent>.Fail(
                    statusCode:StatusCodes.Status400BadRequest,
                    isShow:true,
                    path:"/diet",
                    errors:"Yemek Seçmelisiniz"
                    ).CreateResponseInstance();
            }

            //dietCommandService.AddAsync();



            return Response<NoContent>.Success(StatusCodes.Status201Created).CreateResponseInstance();
        }
    }
}
