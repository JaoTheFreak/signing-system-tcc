using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signing.System.Tcc.Domain.RecordAggregate;

namespace Signing.System.Tcc.Data.EntityConfig
{
    public class RecordEntityConfig : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            //Primary Key
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd().UseNpgsqlIdentityColumn();

            builder.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("NOW()");
        }
    }
}
