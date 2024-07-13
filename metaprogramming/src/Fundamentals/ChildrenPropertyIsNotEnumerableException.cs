namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when a children property is not enumerable.
/// </summary>
public class ChildrenPropertyIsNotEnumerableException : Exception
{
   /// <summary>
   ///    Initializes a new instance of the <see cref="ChildrenPropertyIsNotEnumerableException" /> class.
   /// </summary>
   /// <param name="property"><see cref="PropertyPath" /> that is wrong type.</param>
   public ChildrenPropertyIsNotEnumerableException(PropertyPath property)
      : base($"Property at '{property.Path}' is not of enumerable type.")
   {
   }
}