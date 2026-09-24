using Microsoft.EntityFrameworkCore;

namespace ProductionControlSystem.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyUtcDateTimeConversions();
        modelBuilder.ApplyNoPhysicalCascadeDeletes();
    }
}
