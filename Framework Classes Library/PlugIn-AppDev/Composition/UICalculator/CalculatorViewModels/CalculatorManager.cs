using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public class CalculatorManager
	{
		private readonly CalculatorImport _calculatorImport;

		public CalculatorManager()
		{
			_calculatorImport = new CalculatorImport();
			_calculatorImport.ImportsSatisfied += (sender, args) => OnImportsSatisfied(args);
		}

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(_calculatorImport);
			}
		}

		public IEnumerable<IOperation> GetOperators() => _calculatorImport.Calculator.Value.GetOperations();

		public double InvokeCalculator(IOperation operation, double[] operands)
			=> _calculatorImport.Calculator.Value.Operate(operation, operands);

		private void OnImportsSatisfied(ImportEventArgs importEventArgs)
			=> ImportsSatisfied?.Invoke(this, importEventArgs);
	}
}