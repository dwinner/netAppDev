namespace Fundamentals.Compliance;

/// <summary>
///    Represents a type of compliance metadata.
/// </summary>
/// <param name="Value">Underlying value.</param>
public record ComplianceMetadataType(Guid Value) : ConceptAs<Guid>(Value)
{
    /// <summary>
    ///    Personal Identifiable Information according to the definition in GDPR.
    /// </summary>
    public static readonly ComplianceMetadataType Pii = new(Guid.Parse("cae5580e-83d6-44dc-9d7a-a72e8a2f17d7"));

    /// <summary>
    ///    Convert from a <see cref="string" /> representation of a <see cref="Guid" /> to
    ///    <see cref="ComplianceMetadataType" />.
    /// </summary>
    /// <param name="value"><see cref="string" /> representation.</param>
    public static implicit operator ComplianceMetadataType(string value) => new(Guid.Parse(value));

    /// <summary>
    ///    Convert from <see cref="Guid" /> to <see cref="ComplianceMetadataType" />.
    /// </summary>
    /// <param name="value)"><see cref="Guid" /> to convert from.</param>
    /// <param name="value">GUID value</param>
    public static implicit operator ComplianceMetadataType(Guid value) => new(value);
}