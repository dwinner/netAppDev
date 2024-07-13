namespace Fundamentals;

/// <summary>
///    Defines a segment within a <see cref="PropertyPath" />.
/// </summary>
public interface IPropertyPathSegment
{
    /// <summary>
    ///    Gets the value that represents the segment.
    /// </summary>
    string Value { get; }
}