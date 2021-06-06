using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Person;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.ControllerBases;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Entities.Enums;
using DietPlanner.Server.Filters;
using DietPlanner.Shared.ExtensionMethods;
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
        private readonly IGenericCommandService<Dietician> genericDieticianCommandService;
        private readonly IPersonService personService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public AdminController(
            IGenericQueryService<Dietician> genericDieticianService,
            IGenericCommandService<Dietician> genericDieticianCommandService,
            IPersonService personService,
            IRoleService roleService,
            IMapper mapper)
        {
            this.genericDieticianService = genericDieticianService;
            this.genericDieticianCommandService = genericDieticianCommandService;
            this.personService = personService;
            this.roleService = roleService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDietician()
        {
            return Response<IEnumerable<UserDto>>
                  .Success(await genericDieticianService.GetAllAsync<UserDto>(), StatusCodes.Status200OK)
                  .CreateResponseInstance();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDietician(UserCreateDto dto)
        {
            UserDto found = await personService.GetPersonByIdentityNumber(PersonType.Dietician, dto.IdentityNumber);
            if (found.IsNotNull())
                return Response<UserDto>.Fail(
                           statusCode: StatusCodes.Status400BadRequest,
                           isShow: true,
                           path: "[POST] api/admin/createdietician",
                           errors: "Girilen kimlik no ile kayıtlı bir diyetisyen zaten mevcut"
                           )
                    .CreateResponseInstance();
            var dieticianRole = await roleService.GetRoleByName(RoleInfo.Dietician);
            dto.RoleId = dieticianRole.Id;
            var dietician = await genericDieticianCommandService.AddAsync(dto);
            await genericDieticianCommandService.Commit();
            var returnModel = mapper.Map<UserDto>(dietician);
            return Response<UserDto>
                .Success(returnModel,StatusCodes.Status201Created)
                .CreateResponseInstance();
        }
    }
}
