using RuleContracts;

namespace BadControlFlow.Rules
{
	internal sealed class SameReturn : IRule
	{
		public string DiagnosticId { get; } = "MPB0005";
		public string Message { get; } = "The method has always the same return value";
		public RuleSeverity Severity { get; } = RuleSeverity.Alarm;
	}
}