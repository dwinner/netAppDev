using RuleContracts;

namespace ArchitectureIssues.Rules
{
	internal sealed class VirtualMethodCallInCtorRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0011";
		public string Message { get; } = "Virtual member called in constructor";
		public RuleSeverity Severity { get; } = RuleSeverity.Warning;
	}
}