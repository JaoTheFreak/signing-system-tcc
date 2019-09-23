using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Signing.System.Tcc.Identity.Models;

namespace Signing.System.Tcc.Identity.Context
{
    public class AppContextIdentity : IdentityDbContext<AppIdentityUser>
    {
        
    }
}