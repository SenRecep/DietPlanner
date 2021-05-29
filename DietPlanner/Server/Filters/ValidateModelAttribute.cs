
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Mvc.Filters;

namespace DietPlanner.Server.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid.IsFalse())
                context.Result = context.GetBadRequestResultErrorDtoForModelState();
        }
    }
}
