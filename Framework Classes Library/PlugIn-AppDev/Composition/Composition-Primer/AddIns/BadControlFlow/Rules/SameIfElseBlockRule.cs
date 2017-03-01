using RuleContracts;

namespace BadControlFlow.Rules
{
	internal sealed class SameIfElseBlockRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0006";
		public string Message { get; } = "The block in the if/else statement are the same";
		public RuleSeverity Severity { get; } = RuleSeverity.Alarm;
	}
}