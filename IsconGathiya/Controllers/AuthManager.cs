using IsconGathiya.Service.Account;
using IsconGathiya.Service.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace IsconGathiya.Controllers

{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthManager : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtTokenRepository>();
            var loginService = context.HttpContext.RequestServices.GetService<IAccountRepository>();

            if (jwtService == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
                return;
            }

            var request = context.HttpContext.Request;
            var token = request.Cookies["AuthValidator"];

            if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {

                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));

                return;
            }
        }
    }
}
