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
		private readonly CalculatorImport _calcImport;

		public CalculatorManager()
		{
			_calcImport = new CalculatorImport();
			_calcImport.ImportsSatisfied += (sender, e) => { ImportsSatisfied?.Invoke(this, e); };
		}

		public event EventHandler<ImportEventArgs> ImportsSatisfied;


		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(_calcImport);
			}
		}

		public IEnumerable<IOperation> GetOperators() => _calcImport.Calculator.Value.GetOperations();

		public double InvokeCalculator(IOperation operation, double[] operands)
			=> _calcImport.Calculator.Value.Operate(operation, operands);
	}
}