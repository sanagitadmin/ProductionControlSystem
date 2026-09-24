namespace ProductionControlSystem.Domain.Abstractions;

/// <summary>
/// Contract for entities that are removed by state rather than physical deletion.
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; }

    DateTime? DeletedAtUtc { get; }
}
