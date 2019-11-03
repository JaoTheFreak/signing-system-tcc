using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Signing.System.Tcc.Domain.UserAggregate;
using Signing.System.Tcc.MVC.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Services
{
    public class AuthService : IAuthenticantionService
    {
        public async Task SignInAsync(User user, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email)                
            };
            
            var claimsIdentity = new ClaimsIdentity(
                claims, Helpers.Helpers.SigningSystemScheme);
                        
            await httpContext.SignInAsync(Helpers.Helpers.SigningSystemScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {            
            await httpContext.SignOutAsync(Helpers.Helpers.SigningSystemScheme);
        }
    }
}
