using System.Collections.Generic;
using System.Composition;
using Numerical.Rules;
using RuleContracts;

namespace Numerical
{
	[Export(typeof(IRuleGroup))]
	public sealed class NumericalRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new FractionLossRule()
		};

		public RuleCategory Category { get; } = RuleCategory.Numerical;
	}
}