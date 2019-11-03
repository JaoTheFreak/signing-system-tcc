using Signing.System.Tcc.Domain.Service;

namespace Signing.System.Tcc.Domain.UserAggregate
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }
    }
}
