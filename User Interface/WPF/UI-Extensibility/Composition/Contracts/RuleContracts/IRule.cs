namespace RuleContracts
{
	public interface IRule
	{
		string DiagnosticId { get; }
		string Message { get; }
		RuleSeverity Severity { get; }
	}
}