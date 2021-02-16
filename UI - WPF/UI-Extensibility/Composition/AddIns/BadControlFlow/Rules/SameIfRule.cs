using RuleContracts;

namespace BadControlFlow.Rules
{
	internal sealed class SameIfRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0004";
		public string Message { get; } = "The code block has the same if-condition";
		public RuleSeverity Severity { get; } = RuleSeverity.Note;
	}
}