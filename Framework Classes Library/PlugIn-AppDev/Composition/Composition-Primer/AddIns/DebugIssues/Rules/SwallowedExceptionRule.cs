using RuleContracts;

namespace DebugIssues.Rules
{
	internal sealed class SwallowedExceptionRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0009";

		public string Message { get; } =
			"The original exception object was swallowed. Stack of original exception could be lost";

		public RuleSeverity Severity { get; } = RuleSeverity.Paranoic;
	}
}