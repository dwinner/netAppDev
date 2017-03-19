using System;
using ArchitectureIssues;
using BadControlFlow;
using Concurrency;
using DebugIssues;
using LinqIssues;
using Numerical;
using RuleImports;
using static System.Console;

namespace ConsoleAppHosting
{
	internal static class Program
	{
		private static void Main()
		{
			var ruleGroupManager = new RuleGroupManager();
			Type[] ruleGroupTypes =
			{
				typeof(ArchitectureRuleGroup),
				typeof(BadControlFlowRuleGroup),
				typeof(ConcurrencyRuleGroup),
				typeof(DebugIssueRuleGroup),
				typeof(LinqRuleGroup),
				typeof(NumericalRuleGroup)
			};
			ruleGroupManager.InitializeContainer(ruleGroupTypes);
			ruleGroupManager.ImportsSatisfied += (sender, eventArgs) => { WriteLine(eventArgs.StatusMessage); };
			var ruleGroups = ruleGroupManager.GetRuleGroups();
			foreach (var ruleGroup in ruleGroups)
			{
				var category = ruleGroup.Value.Category;
				WriteLine(category);
				var rules = ruleGroup.Value.GetRules();
				foreach (var rule in rules)
					WriteLine("\t- {0}", rule.Message);
			}
		}
	}
}