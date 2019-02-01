using System.Collections.Generic;
using System.Composition;
using LinqIssues.Rules;
using RuleContracts;

namespace LinqIssues
{
	[Export(typeof(IRuleGroup))]
	public sealed class LinqRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new LinqTrueForAllRule()
		};

		public RuleCategory Category { get; } = RuleCategory.Linq;
	}
}