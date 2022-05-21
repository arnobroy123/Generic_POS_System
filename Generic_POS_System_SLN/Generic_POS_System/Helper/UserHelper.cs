using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Generic_POS_System.Helper
{
    public class UserHelper
    {
        private IHttpContextAccessor _httpContext;

        public UserHelper(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsLoggedIn()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
