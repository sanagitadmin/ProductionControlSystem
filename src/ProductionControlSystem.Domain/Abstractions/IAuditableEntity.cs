namespace ProductionControlSystem.Domain.Abstractions;

/// <summary>
/// Contract for entities that record creation and update timestamps in UTC.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; }

    DateTime? UpdatedAtUtc { get; }
}
