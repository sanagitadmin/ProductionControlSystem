namespace ProductionControlSystem.Application.Common;

/// <summary>
/// Describes an application error without transport-specific semantics.
/// </summary>
/// <param name="Code">Stable application error code.</param>
/// <param name="Message">Human-readable error message.</param>
public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}
