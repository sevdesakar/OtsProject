using Microsoft.EntityFrameworkCore;

namespace Ots.Api;

public class OtsDbContext : DbContext
{
    public OtsDbContext(DbContextOptions<OtsDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OtsDbContext).Assembly);
        // modelBuilder.ApplyConfiguration(new AccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}
