using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.Data.EntityConfig
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Primary Key
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd().UseNpgsqlIdentityColumn();

            builder.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("NOW()");

            ////Map to our table
            builder.ToTable("Users");
        }
    }
}