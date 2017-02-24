using System.Collections.Generic;
using System.Composition;
using BadControlFlow.Rules;
using RuleContracts;

namespace BadControlFlow
{
	[Export(typeof(IRuleGroup))]
	public sealed class BadControlFlowRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new ExcessiveLogicalCheck(),
			new SameIfElseBlockRule(),
			new SameIfRule(),
			new SameReturn(),
			new WrongNullCheckRule()
		};

		public RuleCategory Category { get; } = RuleCategory.BadControlFlow;
	}
}