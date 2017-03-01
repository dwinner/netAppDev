using System.Collections.Generic;
using System.Composition;
using ArchitectureIssues.Rules;
using RuleContracts;

namespace ArchitectureIssues
{
	[Export(typeof(IRuleGroup))]
	public sealed class ArchitectureRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new VirtualMethodCallInCtorRule()
		};

		public RuleCategory Category { get; } = RuleCategory.Architecture;
	}
}