
using Microsoft.AspNetCore.Authorization;

namespace DietPlanner.ClientShared.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(',', roles);
        }
    }
}
