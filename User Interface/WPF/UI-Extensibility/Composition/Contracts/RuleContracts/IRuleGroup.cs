using System.Collections.Generic;

namespace RuleContracts
{
	public interface IRuleGroup
	{
		IList<IRule> GetRules();
		RuleCategory Category { get; }
	}
}