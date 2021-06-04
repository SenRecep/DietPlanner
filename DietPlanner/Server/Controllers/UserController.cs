using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Shared.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPersonService personService;

        public UserController(IPersonService personService)
        {
            this.personService = personService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            dto.Password = ToPasswordRepository.PasswordCryptographyCombine(dto.Password);
            var found = await personService.LoginAsync(dto);
            if (found is null)
                return Response<UserDto>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "[POST] api/user/login",
                    errors: "Kullanıcı kimlik numarası veya parola hatalı"
                    ).CreateResponseInstance();
            return Response<UserDto>.Success(found, StatusCodes.Status200OK).CreateResponseInstance();
        }
    }
}
