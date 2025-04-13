using Microsoft.EntityFrameworkCore;
using Ots.Api.Domain;
using Ots.Base;

namespace Ots.Api;

public class OtsDbContext : DbContext
{
    public OtsDbContext(DbContextOptions<OtsDbContext> options) : base(options)
    {
    }

    // DbSet tanýmlarý
    public DbSet<Customer> Customers { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityList = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                        (e.State == EntityState.Added ||
                         e.State == EntityState.Modified ||
                         e.State == EntityState.Deleted));

        foreach (var entry in entityList)
        {
            var baseEntity = (BaseEntity)entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    baseEntity.InsertedDate = DateTime.Now;
                    baseEntity.InsertedUser = "system";
                    baseEntity.IsActive = true;
                    break;

                case EntityState.Modified:
                    baseEntity.UpdatedDate = DateTime.Now;
                    baseEntity.UpdatedUser = "system";
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    baseEntity.IsActive = false;
                    baseEntity.UpdatedDate = DateTime.Now;
                    baseEntity.UpdatedUser = "system";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OtsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
