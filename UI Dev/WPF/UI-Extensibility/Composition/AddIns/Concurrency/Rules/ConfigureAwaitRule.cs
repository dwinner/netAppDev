using RuleContracts;

namespace Concurrency.Rules
{
	internal sealed class ConfigureAwaitRule : IRule
	{
		public string DiagnosticId { get; } = "MPB0003";
		public string Message { get; } = "Await expressions on Task or Task<T> should use ConfigureAwait to avoid deadlocks";
		public RuleSeverity Severity { get; } = RuleSeverity.Note;
	}
}