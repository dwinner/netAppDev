namespace Fundamentals;

/// <summary>
///    Exception that gets thrown when a <see cref="ArrayIndexer" /> is missing for a <see cref="PropertyPath" />.
/// </summary>
public class MissingArrayIndexerForPropertyPathException : Exception
{
   /// <summary>
   ///    Initializes a new instance of the <see cref="MissingArrayIndexerForPropertyPathException" /> class.
   /// </summary>
   /// <param name="propertyPath"><see cref="PropertyPath" /> it is missing for.</param>
   public MissingArrayIndexerForPropertyPathException(PropertyPath propertyPath)
      : base($"Missing array indexer for '{propertyPath}'")
   {
   }
}