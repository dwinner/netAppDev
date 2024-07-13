using Fundamentals;

namespace Concepts.Employees;

public record LastName(string Value) : PiiConceptAs<string>(Value);
