using Microsoft.EntityFrameworkCore;
using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Data.Context
{
    public class SigningContext : DbContext
    {
        DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = "Server=127.0.0.1; Port=5434; Database=signing_system;User Id=dev; Password=123@dev;";

            optionsBuilder.UseNpgsql(conn);

            optionsBuilder.EnableSensitiveDataLogging();

            //base.OnConfiguring(optionsBuilder);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}