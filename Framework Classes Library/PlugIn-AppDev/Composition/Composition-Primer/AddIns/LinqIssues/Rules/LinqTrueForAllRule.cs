using RuleContracts;

namespace LinqIssues.Rules
{
	internal sealed class LinqTrueForAllRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0002";
		public string Message { get; } = "List.TrueForAll VS Enumerable.All";
		public RuleSeverity Severity { get; } = RuleSeverity.LookClosely;
	}
}