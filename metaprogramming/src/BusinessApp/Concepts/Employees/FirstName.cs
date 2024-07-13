using Fundamentals;

namespace Concepts.Employees;

public record FirstName(string Value) : PiiConceptAs<string>(Value);
