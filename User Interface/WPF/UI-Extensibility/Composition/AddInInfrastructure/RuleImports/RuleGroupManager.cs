using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using RuleContracts;

namespace RuleImports
{
	public sealed class RuleGroupManager
	{
		private readonly RuleGroupImport _ruleGroupImport = new RuleGroupImport();

		public RuleGroupManager()
		{
			_ruleGroupImport.ImportsSatisfied += (sender, args) => OnImportsSatisfied(args);
		}

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(_ruleGroupImport);
			}
		}

		public IEnumerable<Lazy<IRuleGroup>> GetRuleGroups() => _ruleGroupImport.RuleGroups.ToArray();

		private void OnImportsSatisfied(ImportEventArgs e) => ImportsSatisfied?.Invoke(this, e);
	}
}