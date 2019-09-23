using Microsoft.EntityFrameworkCore;

namespace Signing.System.Tcc.Data.Context
{
    public class SigningContext : DbContext
    {
        public SigningContext(DbContextOptions<SigningContext> databaseOptions) : base(databaseOptions)
        {            
        }
    }
}