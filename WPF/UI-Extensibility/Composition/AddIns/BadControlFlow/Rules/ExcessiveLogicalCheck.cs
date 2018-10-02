using RuleContracts;

namespace BadControlFlow.Rules
{
	internal sealed class ExcessiveLogicalCheck : IRule
	{
		public string DiagnosticId { get; } = "MPB0008";
		public string Message { get; } = "The expression is excessive or contains a misprint";
		public RuleSeverity Severity { get; } = RuleSeverity.Warning;
	}
}