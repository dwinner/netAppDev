using Fundamentals;

namespace Concepts.Employees;

public record SocialSecurityNumber(string Value) : PiiConceptAs<string>(Value);