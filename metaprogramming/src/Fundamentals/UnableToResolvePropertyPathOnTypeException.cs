namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when property path is not possible to be resolved on a type.
/// </summary>
public class UnableToResolvePropertyPathOnTypeException : Exception
{
    /// <summary>
    ///    Initializes a new instance of the <see cref="UnableToResolvePropertyPathOnTypeException" /> class.
    /// </summary>
    /// <param name="type">Type that does not hold the property path.</param>
    /// <param name="path">The <see cref="PropertyPath" /> that is not possible to resolve.</param>
    public UnableToResolvePropertyPathOnTypeException(Type type, PropertyPath path) 
       : base("Unable to resolve property path '${path}' on type '${type.FullName}'")
   {
   }
}