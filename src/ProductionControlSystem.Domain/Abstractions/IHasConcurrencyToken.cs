namespace ProductionControlSystem.Domain.Abstractions;

/// <summary>
/// Contract for entities that expose an application-level optimistic concurrency token.
/// </summary>
public interface IHasConcurrencyToken
{
    string ConcurrencyToken { get; }
}
