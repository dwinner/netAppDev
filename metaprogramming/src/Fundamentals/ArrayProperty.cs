namespace Fundamentals;

/// <summary>
///    Represents a <see cref="IPropertyPathSegment" /> for an array.
/// </summary>
/// <param name="Value">Identifier of the array indexer.</param>
public record ArrayProperty(string Value) : IPropertyPathSegment
{
    /// <inheritdoc />
    public override string ToString() => $"[{Value}]";
}