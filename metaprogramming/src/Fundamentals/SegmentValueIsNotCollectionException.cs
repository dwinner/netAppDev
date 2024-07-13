namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when a <see cref="IPropertyPathSegment">segment</see> within a
///    <see cref="PropertyPath" /> is not an ExpandoObject.
/// </summary>
public class SegmentValueIsNotCollectionException : Exception
{
    /// <summary>
    ///    Initializes a new instance of the <see cref="SegmentValueIsNotCollectionException" /> class.
    /// </summary>
    /// <param name="propertyPath"><see cref="PropertyPath" />.</param>
    /// <param name="segment"><see cref="IPropertyPathSegment" />.</param>
    public SegmentValueIsNotCollectionException(PropertyPath propertyPath, IPropertyPathSegment segment)
      : base($"Segment '{segment.Value}' in '{propertyPath}' is not a collection of ExpandoObject")
   {
   }
}