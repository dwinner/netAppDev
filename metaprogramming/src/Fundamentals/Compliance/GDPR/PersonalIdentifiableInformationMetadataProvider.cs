using System.Reflection;

namespace Fundamentals.Compliance.GDPR;

/// <summary>
///    Represents a metadata provider for PII.
/// </summary>
public class PersonalIdentifiableInformationMetadataProvider :
   ICanProvideComplianceMetadataForType,
   ICanProvideComplianceMetadataForProperty
{
   /// <inheritdoc />
   public bool CanProvide(PropertyInfo property) =>
      property.GetCustomAttribute<PersonalIdentifiableInformationAttribute>() != default ||
      property.DeclaringType?.GetCustomAttribute<PersonalIdentifiableInformationAttribute>() != default ||
      CanProvide(property.PropertyType);

   /// <inheritdoc />
   public ComplianceMetadata Provide(PropertyInfo property)
   {
      if (!CanProvide(property))
      {
         throw new NoComplianceMetadataForProperty(property);
      }

      var details = property.GetComplianceMetadataDetails();
      if (string.IsNullOrEmpty(details))
      {
         details = property.GetCustomAttribute<PersonalIdentifiableInformationAttribute>()!.ReasonForCollecting;
      }

      return new ComplianceMetadata(ComplianceMetadataType.Pii, details);
   }

   /// <inheritdoc />
   public bool CanProvide(Type type) => type.Implements(typeof(IHoldPersonalIdentifiableInformation)) ||
                                        type.GetCustomAttribute<PersonalIdentifiableInformationAttribute>() != default;

   /// <inheritdoc />
   public ComplianceMetadata Provide(Type type)
   {
      if (!CanProvide(type))
      {
         throw new NoComplianceMetadataForTypeException(type);
      }

      return new ComplianceMetadata(ComplianceMetadataType.Pii, type.GetComplianceMetadataDetails());
   }
}