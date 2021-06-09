using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Food;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(RoleInfo.Dietician)]
    public class FoodController : ControllerBase
    {
        private readonly IGenericCommandService<Food> foodCommandService;
        private readonly IGenericQueryService<Food> foodQueryService;

        public FoodController(IGenericCommandService<Food> foodCommandService,IGenericQueryService<Food> foodQueryService)
        {
            this.foodCommandService = foodCommandService;
            this.foodQueryService = foodQueryService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FoodCreateDto foodCreateDto)
        {
            await foodCommandService.AddAsync(foodCreateDto);
            await foodCommandService.Commit();
            return Response<NoContent>.Success(StatusCodes.Status201Created).CreateResponseInstance();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var foods = await foodQueryService.GetAllAsync<DietFoodCreateDto>();
            return Response<IEnumerable<DietFoodCreateDto>>.Success(foods).CreateResponseInstance();
        }
    }
}
