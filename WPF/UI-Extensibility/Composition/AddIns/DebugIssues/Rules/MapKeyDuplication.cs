using RuleContracts;

namespace DebugIssues.Rules
{
	internal sealed class MapKeyDuplication : IRule
	{
		public string DiagnosticId { get; } = "MPB0010";
		public string Message { get; } = "There is an item with the same key";
		public RuleSeverity Severity { get; } = RuleSeverity.FatalError;
	}
}