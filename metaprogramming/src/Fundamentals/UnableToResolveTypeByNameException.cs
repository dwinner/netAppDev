namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when a type is not possible to be resolved by its name.
/// </summary>
public class UnableToResolveTypeByNameException : ArgumentException
{
    /// <summary>
    ///    Initializes a new instance of the <see cref="UnableToResolveTypeByNameException" /> class.
    /// </summary>
    /// <param name="typeName">Name of the type that was not possible to resolve.</param>
    public UnableToResolveTypeByNameException(string typeName)
      : base($"Unable to resolve '{typeName}'.")
   {
   }
}