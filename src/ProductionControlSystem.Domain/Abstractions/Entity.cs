namespace ProductionControlSystem.Domain.Abstractions;

/// <summary>
/// Base type for domain entities with a strongly typed identity.
/// </summary>
/// <typeparam name="TId">The entity identity type.</typeparam>
public abstract class Entity<TId>
    where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; protected init; }
}
