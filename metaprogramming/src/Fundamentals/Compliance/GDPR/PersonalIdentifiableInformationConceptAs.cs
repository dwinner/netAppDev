namespace Fundamentals.Compliance.GDPR;

/// <summary>
///    Represents a <see cref="ConceptAs{T}" /> that holds PII according to the definition of GDPR.
/// </summary>
/// <param name="Value">Underlying value.</param>
/// <typeparam name="T">Type of the underlying value.</typeparam>
public record PersonalIdentifiableInformationConceptAs<T>(T Value) :
   ConceptAs<T>(Value),
   IHoldPersonalIdentifiableInformation
{
   /// <summary>
   ///    Gets the details for the PII.
   /// </summary>
   public virtual string Details => string.Empty;
}