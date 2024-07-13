namespace Fundamentals.Compliance;

/// <summary>
///    Exception that gets thrown when where is no <see cref="ComplianceMetadata" /> associated with a property.
/// </summary>
public class NoComplianceMetadataForTypeException : Exception
{
   /// <summary>
   ///    Initializes a new instance of the <see cref="NoComplianceMetadataForProperty" /> class.
   /// </summary>
   /// <param name="type"><see cref="Type" /> that does not have compliance metadata.</param>
   public NoComplianceMetadataForTypeException(Type type)
      : base($"Types '{type.FullName}'  does not have any compliance metadata.")
   {
   }
}