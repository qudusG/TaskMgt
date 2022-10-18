using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskManagement
{
    public static class Helper
    {
        public static string GetAuthenticatedUserId(this ClaimsPrincipal claims)
        {
            var claimsIdentity = claims.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst("id").Value;
        }
    }
}
