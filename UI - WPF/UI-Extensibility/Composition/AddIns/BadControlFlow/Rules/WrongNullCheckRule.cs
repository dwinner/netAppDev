using RuleContracts;

namespace BadControlFlow.Rules
{
	internal sealed class WrongNullCheckRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0007";
		public string Message { get; } = "Incorrect variable is compared to null";
		public RuleSeverity Severity { get; } = RuleSeverity.Error;
	}
}