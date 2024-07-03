using Fundamentals;

namespace BeyondInheritance.EventSourcing;

public record EventSequenceNumber(ulong Value) : ConceptAs<ulong>(Value)
{
    public static implicit operator EventSequenceNumber(ulong value) => new(value);
}
