
using DietPlanner.DTO.Response;
using DietPlanner.Server.BLL.ExtensionMethods;

using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult CreateResponseInstance<T>(Response<T> response) => response.CreateResponseInstance();
    }
}
