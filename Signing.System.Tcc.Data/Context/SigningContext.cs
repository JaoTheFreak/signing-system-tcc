using Microsoft.EntityFrameworkCore;
using Signing.System.Tcc.Domain.UserAggregate;
using System;
using System.Linq;
using Signing.System.Tcc.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace Signing.System.Tcc.Data.Context
{
    public class SigningContext : DbContext
    {
        DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Environment.GetEnvironmentVariable("CONN_STRING");

            optionsBuilder.UseNpgsql(connString);

            optionsBuilder.EnableSensitiveDataLogging();         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.ForNpgsqlUseIdentityColumns();

            // Setting decimal precision
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 2)";
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #region Applying Entity Config

            #endregion

            #region Corretions in Auto Increment Ids
            // User Id
            modelBuilder.HasSequence<int>("User")
                .StartsAt(12)
                .IncrementsBy(1);

            modelBuilder.Entity<User>()
                .Property(o => o.Id)
                .HasDefaultValueSql("nextval('\"User\"')");
            #endregion


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();

            addedEntities.ForEach(e =>
            {
                var propName = "CreatedAt";

                if (e.Metadata.FindProperty(propName) != null)
                    e.Property(propName).CurrentValue = DateTime.Now;
            });

            var editedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

            editedEntities.ForEach(e =>
            {
                var propName = "UpdatedAt";

                if (e.Metadata.FindProperty(propName) != null)
                    e.Property(propName).CurrentValue = DateTime.Now;
            });

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            addedEntities.ForEach(e =>
            {
                var propName = "CreatedAt";

                if (e.Metadata.FindProperty(propName) != null)
                    e.Property(propName).CurrentValue = DateTime.Now;
            });

            var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            editedEntities.ForEach(e =>
            {
                var propName = "UpdatedAt";

                if (e.Metadata.FindProperty(propName) != null)
                    e.Property(propName).CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}