using System.Threading.Tasks;

using DietPlanner.DTO.Response;
using DietPlanner.DTO.Test;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.ControllerBases;
using DietPlanner.Server.Filters;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class ValuesController : CustomControllerBase
    {
        public IActionResult Get()
        {
            return CreateResponseInstance(Response<TestModel>.Success(new() { Data = "Vay be" }, StatusCodes.Status200OK));
        }
    }
}
