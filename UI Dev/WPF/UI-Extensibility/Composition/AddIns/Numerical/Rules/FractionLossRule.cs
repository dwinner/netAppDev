using RuleContracts;

namespace Numerical.Rules
{
	internal sealed class FractionLossRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0001";
		public string Message { get; } = "Possible loss of fraction";
		public RuleSeverity Severity { get; } = RuleSeverity.EpicFail;
	}
}