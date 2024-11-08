namespace Fundamentals;

/// <summary>
///    Defines a system working with <see cref="ArrayIndexer" />.
/// </summary>
public interface IArrayIndexers
{
    /// <summary>
    ///    Gets all <see cref="ArrayIndexer" />.
    /// </summary>
    /// <returns>Collection of <see cref="ArrayIndexer" />.</returns>
    IEnumerable<ArrayIndexer> All { get; }

    /// <summary>
    ///    Get an <see cref="ArrayIndexer" /> for a <see cref="PropertyPath" />.
    /// </summary>
    /// <param name="propertyPath"><see cref="PropertyPath" /> to get for.</param>
    /// <returns>The <see cref="ArrayIndexer" />.</returns>
    ArrayIndexer GetFor(PropertyPath propertyPath);

    /// <summary>
    ///    Check if there is an <see cref="ArrayIndexer" /> for a <see cref="PropertyPath" />.
    /// </summary>
    /// <param name="propertyPath"><see cref="PropertyPath" /> to check for.</param>
    /// <returns>True if it has it, false if not.</returns>
    bool HasFor(PropertyPath propertyPath);
}