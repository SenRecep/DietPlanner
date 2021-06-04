using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.ControllerBases;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [CustomAuthorize(RoleInfo.Admin)]
    public class AdminController : CustomControllerBase
    {
        private readonly IGenericQueryService<Dietician> genericDieticianService;

        public AdminController(IGenericQueryService<Dietician> genericDieticianService)
        {
            this.genericDieticianService = genericDieticianService;
        }
        public async Task<IActionResult> GetAllDietician()
        {
            return Response<IEnumerable<UserDto>>
                  .Success(await genericDieticianService.GetAllAsync<UserDto>(), StatusCodes.Status200OK)
                  .CreateResponseInstance();
        }
    }
}
