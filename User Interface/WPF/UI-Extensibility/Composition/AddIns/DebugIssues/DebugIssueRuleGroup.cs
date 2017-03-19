using System.Collections.Generic;
using System.Composition;
using DebugIssues.Rules;
using RuleContracts;

namespace DebugIssues
{
	[Export(typeof(IRuleGroup))]
	public sealed class DebugIssueRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new MapKeyDuplication(),
			new SwallowedExceptionRule()
		};

		public RuleCategory Category { get; } = RuleCategory.DebugIssue;
	}
}