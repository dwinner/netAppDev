using System;
using System.Collections.Generic;
using System.Composition;
using RuleContracts;

namespace RuleImports
{
	public sealed class RuleGroupImport
	{
		[ImportMany]
		public IEnumerable<Lazy<IRuleGroup>> RuleGroups { get; set; }

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		private void OnImportsSatisfied(ImportEventArgs e) => ImportsSatisfied?.Invoke(this, e);

		[OnImportsSatisfied]
		public void OnSatisfy() => OnImportsSatisfied(new ImportEventArgs {StatusMessage = "Rule group imports successfully"});
	}
}