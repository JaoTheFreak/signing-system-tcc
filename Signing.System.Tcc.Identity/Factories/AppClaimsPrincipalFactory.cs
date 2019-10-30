using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Signing.System.Tcc.Identity.Models;

namespace Signing.System.Tcc.Identity.Factories
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppIdentityUser>
    {
        private RoleManager<AppIdentityUser> RoleManager { get; }        

        public AppClaimsPrincipalFactory(UserManager<AppIdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor, RoleManager<AppIdentityUser> roleManager) : base(userManager, optionsAccessor)
        {
            RoleManager = roleManager;            
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppIdentityUser user)
        {
            var principalClaims = await base.CreateAsync(user);

            return principalClaims;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppIdentityUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);

            var userClaims = await UserManager.GetClaimsAsync(user);

            var userRoles = await UserManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var userRole in userRoles)
            {
                var role = await RoleManager.FindByNameAsync(userRole);
                
                var claims = await RoleManager.GetClaimsAsync(role);

                roleClaims.AddRange(claims);
            }

            // var claimsInJwt = new List<Claim>
            // {
            //     new Claim("CompanyId", user.CompanyId.ToString()),
            //     new Claim("UserEmail", user.Email),
            //     new Claim("UserId", user.Id.ToString())
            // };

            // var jsonWebToken = _tokenService.GenerateToken(claimsInJwt);

            // var customClaims = new List<Claim>
            // {
            //     new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
            //     new Claim("CompanyId", user.CompanyId.ToString()),
            //     new Claim(ClaimTypes.Email, user.Email),
            //     new Claim("PicturePath", user.PicturePath),
            //     new Claim(TokenType.Jwt.ToString(), jsonWebToken.Value),
            //     new Claim("DisplayName",user.DisplayName)
            // };

            // claimsIdentity.AddClaims(userClaims);

            // claimsIdentity.AddClaims(roleClaims);

            // claimsIdentity.AddClaims(customClaims);

            return claimsIdentity;
        }

    }
}