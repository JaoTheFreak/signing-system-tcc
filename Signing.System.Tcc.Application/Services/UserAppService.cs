using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Application.Services
{
    public class UserAppService : AppService<User>, IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService service) : base(service)
        {
            _userService = service;
        }
    }
}
