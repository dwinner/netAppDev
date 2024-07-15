using Fundamentals.Compliance.GDPR;

namespace MetricsSample;

/// <summary>
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="SocialSecurityNumber"></param>
public record Employee(
    [PersonalIdentifiableInformation("Needed for registration")]
    string FirstName,
    [PersonalIdentifiableInformation("Needed for registration")]
    string LastName,
    [PersonalIdentifiableInformation("Needed for uniquely identifying an employee")]
    string SocialSecurityNumber);