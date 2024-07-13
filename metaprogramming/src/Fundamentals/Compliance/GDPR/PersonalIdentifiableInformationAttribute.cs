namespace Fundamentals.Compliance.GDPR;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PersonalIdentifiableInformationAttribute(string reasonForCollecting = "") : Attribute
{
   public string ReasonForCollecting { get; } = reasonForCollecting;
}