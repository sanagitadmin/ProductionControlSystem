using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductionControlSystem.Infrastructure.Persistence;

namespace ProductionControlSystem.Tests.Infrastructure.Persistence;

public sealed class ModelBuilderPersistenceExtensionsTests
{
    [Fact]
    public void ApplyNoPhysicalCascadeDeletes_ConvertsCascadeRelationshipsToClientCascade()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.Entity<TestParent>();
        modelBuilder.Entity<TestChild>()
            .HasOne(child => child.Parent)
            .WithMany(parent => parent.Children)
            .HasForeignKey(child => child.ParentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyNoPhysicalCascadeDeletes();

        var foreignKey = modelBuilder.Model.FindEntityType(typeof(TestChild))!.GetForeignKeys().Single();
        Assert.Equal(DeleteBehavior.ClientCascade, foreignKey.DeleteBehavior);
    }

    [Fact]
    public void ApplyUtcDateTimeConversions_AddsConvertersForDateTimeProperties()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.Entity<TestTimestamped>(entity =>
        {
            entity.Property(timestamped => timestamped.CreatedAtUtc);
            entity.Property(timestamped => timestamped.UpdatedAtUtc);
        });

        modelBuilder.ApplyUtcDateTimeConversions();

        var entityType = modelBuilder.Model.FindEntityType(typeof(TestTimestamped))!;
        var requiredConverter = entityType.FindProperty(nameof(TestTimestamped.CreatedAtUtc))!.GetValueConverter();
        var optionalConverter = entityType.FindProperty(nameof(TestTimestamped.UpdatedAtUtc))!.GetValueConverter();

        Assert.NotNull(requiredConverter);
        Assert.NotNull(optionalConverter);
        var convertedRequired = Assert.IsType<DateTime>(requiredConverter.ConvertFromProvider(new DateTime(2026, 9, 24, 10, 0, 0)));
        Assert.Equal(DateTimeKind.Utc, convertedRequired.Kind);
        var convertedOptional = Assert.IsType<DateTime>(optionalConverter.ConvertFromProvider(new DateTime(2026, 9, 24, 10, 0, 0)));
        Assert.Equal(DateTimeKind.Utc, convertedOptional.Kind);
    }

    private sealed class TestParent
    {
        public int Id { get; set; }
        public List<TestChild> Children { get; } = [];
    }

    private sealed class TestChild
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public TestParent Parent { get; set; } = null!;
    }

    private sealed class TestTimestamped
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
