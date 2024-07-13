using System.Reflection;

namespace Fundamentals.Compliance;

/// <summary>
///    Defines a provider of <see cref="ComplianceMetadata" /> for <see cref="PropertyInfo">types</see>.
/// </summary>
public interface ICanProvideComplianceMetadataForProperty
{
    /// <summary>
    ///    Checks whether it can provide for the type.
    /// </summary>
    /// <param name="property"><see cref="Type" /> to check for.</param>
    /// <returns>True if it can provide, false if not.</returns>
    bool CanProvide(PropertyInfo property);

    /// <summary>
    ///    Provide the metadata for the type.
    /// </summary>
    /// <param name="property"><see cref="Type" /> to provide for.</param>
    /// <returns>Provided <see cref="ComplianceMetadata" />.</returns>
    ComplianceMetadata Provide(PropertyInfo property);
}