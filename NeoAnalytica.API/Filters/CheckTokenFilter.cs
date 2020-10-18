using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace NeoAnalytica.API.Filters
{
    public class CheckToken : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check token of authenticated user to store userId, before the action executes.
            var accessToken = await context.HttpContext.GetTokenAsync("access_token");
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = tokenHandler.ReadJwtToken(accessToken);
            int userId = 0;
            var nameId = claims.Claims.Where(x => x.Type == "nameid").FirstOrDefault();
            int.TryParse(nameId.Value, out userId);
            context.HttpContext.Items["userId"] = userId;
            var resultContext = await next();
        }
    }
}
