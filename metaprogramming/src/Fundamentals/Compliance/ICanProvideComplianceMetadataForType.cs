namespace Fundamentals.Compliance;

/// <summary>
///    Defines a provider of <see cref="ComplianceMetadata" /> for <see cref="Type">types</see>.
/// </summary>
public interface ICanProvideComplianceMetadataForType
{
    /// <summary>
    ///    Checks whether it can provide for the type.
    /// </summary>
    /// <param name="type"><see cref="Type" /> to check for.</param>
    /// <returns>True if it can provide, false if not.</returns>
    bool CanProvide(Type type);

    /// <summary>
    ///    Provide the metadata for the type.
    /// </summary>
    /// <param name="type"><see cref="Type" /> to provide for.</param>
    /// <returns>Provided <see cref="ComplianceMetadata" />.</returns>
    ComplianceMetadata Provide(Type type);
}