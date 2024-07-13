namespace Fundamentals;

/// <summary>
///    Represents a <see cref="IPropertyPathSegment" /> for a property name.
/// </summary>
/// <param name="Value">Name of the property.</param>
public record PropertyName(string Value) : IPropertyPathSegment
{
   /// <inheritdoc />
   public override string ToString() => Value;
}