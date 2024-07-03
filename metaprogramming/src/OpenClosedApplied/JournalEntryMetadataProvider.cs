using Fundamentals.Compliance;

namespace OpenClosedApplied;

public class JournalEntryMetadataProvider : ICanProvideComplianceMetadataForType
{
   public bool CanProvide(Type type) => type == typeof(JournalEntry);

   public ComplianceMetadata Provide(Type type) => new("7242aed8-8d70-49df-8713-eea45e2764d4", "Journal entry");
}