using Microsoft.AspNetCore.Http;
using Signing.System.Tcc.Domain.UserAggregate;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Interfaces
{
    public interface IAuthenticantionService
    {
        Task SignInAsync(User user, HttpContext httpContext);
        Task SignOutAsync(HttpContext httpContext);
    }
}
