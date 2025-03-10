using System.Reflection;

namespace Fundamentals.Compliance;

/// <summary>
///    Exception that gets thrown when where is no <see cref="ComplianceMetadata" /> associated with a property.
/// </summary>
public class NoComplianceMetadataForProperty : Exception
{
   /// <summary>
   ///    Initializes a new instance of the <see cref="NoComplianceMetadataForProperty" /> class.
   /// </summary>
   /// <param name="property"><see cref="PropertyInfo" /> that does not have compliance metadata.</param>
   public NoComplianceMetadataForProperty(PropertyInfo property)
      : base(
         $"Property '{property.Name}' on type '{property.DeclaringType?.FullName}' does not have any compliance metadata.")
   {
   }
}