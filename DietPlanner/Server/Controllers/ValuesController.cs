using System.Threading.Tasks;

using DietPlanner.DTO.Test;

using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult(new TestModel() { Data="PATATES"}));
        }
    }
}
