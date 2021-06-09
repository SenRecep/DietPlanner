using System;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
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
        private readonly IGenericCommandService<Diet> dietCommandService;
        private readonly IGenericCommandRepository<DietFood> genericDietFoodCommandRepository;

        public DietController(IGenericCommandService<Diet> dietCommandService, IGenericCommandRepository<DietFood> genericDietFoodCommandRepository)
        {
            this.dietCommandService = dietCommandService;
            this.genericDietFoodCommandRepository = genericDietFoodCommandRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DietCreateDto dietCreateDto)
        {
            if (dietCreateDto.SimpleDietFoods.IsNull() || !dietCreateDto.SimpleDietFoods.Any())
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status400BadRequest,
                    isShow: true,
                    path: "/diet",
                    errors: "Yemek Seçmelisiniz"
                    ).CreateResponseInstance();

            dietCreateDto.CreateUserId = Response.GetUserId();
            var diet = await dietCommandService.AddAsync(dietCreateDto);
            var commitState = await dietCommandService.Commit();

            if (!commitState)
                return Response<NoContent>.Fail(
                   statusCode: StatusCodes.Status500InternalServerError,
                   isShow: true,
                   path: "/diet",
                   errors: "Diyet olusturulurken bir hata ile karsilasildi"
                   ).CreateResponseInstance();


            var dietfoods = dietCreateDto.SimpleDietFoods.Select(food =>
             {
                 return FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, dietCreateDto.CreateUserId)
                 .GiveAValue(x => x.CreatedTime, DateTime.Now)
                 .GiveAValue(x => x.DietId, diet.Id)
                 .GiveAValue(x => x.FoodId, food.Id)
                 .Take();
             });

            await genericDietFoodCommandRepository.AddRangeAsync(dietfoods);
            commitState = await genericDietFoodCommandRepository.Commit();

            if (!commitState)
                return Response<NoContent>.Fail(
                   statusCode: StatusCodes.Status500InternalServerError,
                   isShow: true,
                   path: "/diet",
                   errors: "Diyet olusturulurken bir hata ile karsilasildi"
                   ).CreateResponseInstance();

            return Response<NoContent>.Success(StatusCodes.Status201Created).CreateResponseInstance();
        }
    }
}
