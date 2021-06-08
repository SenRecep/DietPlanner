using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace DietPlanner.Server.BLL.ExtensionMethods
{
   public static class HttpResponseExtensionMethods
    {
        public static Guid GetUserId(this HttpResponse httpResponse) => Guid.Parse(httpResponse.Headers["userId"].ToString());
        public static string GetUserRole(this HttpResponse httpResponse) => httpResponse.Headers["role"].ToString();
    }
}
