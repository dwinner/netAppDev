using System.Collections.Generic;
using System.Composition;
using Concurrency.Rules;
using RuleContracts;

namespace Concurrency
{
	[Export(typeof(IRuleGroup))]
	public sealed class ConcurrencyRuleGroup : IRuleGroup
	{
		public IList<IRule> GetRules() => new List<IRule>
		{
			new ConfigureAwaitRule()
		};

		public RuleCategory Category { get; } = RuleCategory.Concurrency;
	}
}