namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when a children property is not enumerable for a specific type.
/// </summary>
public class ChildrenPropertyIsNotEnumerableForTypeException : Exception
{
   /// <summary>
   ///    Initializes a new instance of the <see cref="ChildrenPropertyIsNotEnumerableException" /> class.
   /// </summary>
   /// <param name="type">Type that is the root of the <see cref="PropertyPath" />.</param>
   /// <param name="property"><see cref="PropertyPath" /> that is wrong type.</param>
   public ChildrenPropertyIsNotEnumerableForTypeException(Type type, PropertyPath property)
      : base($"Property at '{property.Path}' on '{type.FullName}' is not of enumerable type.")
   {
   }
}