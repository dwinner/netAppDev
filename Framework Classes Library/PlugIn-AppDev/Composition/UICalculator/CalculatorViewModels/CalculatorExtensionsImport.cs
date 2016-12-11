using System;
using System.Collections.Generic;
using System.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorExtensionsImport
	{
		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		[ImportMany]
		public IEnumerable<Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute>> CalculatorExtensions { get; set; }

		[OnImportsSatisfied]
		public void OnImportsSatisfied()
			=> ImportsSatisfied?.Invoke(
					this, new ImportEventArgs {StatusMessage = "ICalculatorExtension imports successful"});
	}
}