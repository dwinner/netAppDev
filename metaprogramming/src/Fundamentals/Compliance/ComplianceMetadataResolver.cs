using System.Reflection;

namespace Fundamentals.Compliance;

/// <summary>
///    Represents an implementation of <see cref="IComplianceMetadataResolver" />.
/// </summary>
public class ComplianceMetadataResolver : IComplianceMetadataResolver
{
   private readonly IEnumerable<ICanProvideComplianceMetadataForProperty> _propertyProviders;
   private readonly IEnumerable<ICanProvideComplianceMetadataForType> _typeProviders;

   /// <summary>
   ///    Initializes a new instance of the <see cref="ComplianceMetadataResolver" />.
   /// </summary>
   /// <param name="typeProviders">Type providers.</param>
   /// <param name="propertyProviders">Property providers.</param>
   public ComplianceMetadataResolver(
      IInstancesOf<ICanProvideComplianceMetadataForType> typeProviders,
      IInstancesOf<ICanProvideComplianceMetadataForProperty> propertyProviders)
   {
      _typeProviders = typeProviders.ToArray();
      _propertyProviders = propertyProviders.ToArray();
   }

   /// <inheritdoc />
   public bool HasMetadataFor(Type type) => 
      _typeProviders.Any(forType => forType.CanProvide(type));

   /// <inheritdoc />
   public bool HasMetadataFor(PropertyInfo property) =>
      _propertyProviders.Any(forProperty => forProperty.CanProvide(property));

   /// <inheritdoc />
   public IEnumerable<ComplianceMetadata> GetMetadataFor(Type type)
   {
      ThrowIfNoComplianceMetadataForType(type);
      return _typeProviders
         .Where(forType => forType.CanProvide(type))
         .Select(forType => forType.Provide(type))
         .ToArray();
   }

   /// <inheritdoc />
   public IEnumerable<ComplianceMetadata> GetMetadataFor(PropertyInfo property)
   {
      ThrowIfNoComplianceMetadataForProperty(property);
      return _propertyProviders
         .Where(_ => _.CanProvide(property))
         .Select(_ => _.Provide(property))
         .ToArray();
   }

   private void ThrowIfNoComplianceMetadataForType(Type type)
   {
      if (!HasMetadataFor(type))
      {
         throw new NoComplianceMetadataForTypeException(type);
      }
   }

   private void ThrowIfNoComplianceMetadataForProperty(PropertyInfo property)
   {
      if (!HasMetadataFor(property))
      {
         throw new NoComplianceMetadataForProperty(property);
      }
   }
}