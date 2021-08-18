using System;
using System.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorImport
	{
		[Import]
		public Lazy<ICalculator> Calculator { get; set; }

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		[OnImportsSatisfied]
		public void OnImportsSatisfied()
			=> ImportsSatisfied?.Invoke(this, new ImportEventArgs {StatusMessage = "ICalculator import successful"});
	}
}