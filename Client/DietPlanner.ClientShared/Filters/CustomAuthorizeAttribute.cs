using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;

namespace DietPlanner.ClientShared.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public List<string> RoleList { get; set; }
        public CustomAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(',', roles);
            RoleList = roles.ToList();
        }
    }
}
