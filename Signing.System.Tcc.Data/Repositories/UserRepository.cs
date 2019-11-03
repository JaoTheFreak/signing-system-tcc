using Microsoft.EntityFrameworkCore;
using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
