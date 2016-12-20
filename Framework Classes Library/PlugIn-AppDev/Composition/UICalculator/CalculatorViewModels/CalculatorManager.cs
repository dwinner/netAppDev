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
		private CalculatorImport _calcImport;
		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		public CalculatorManager()
		{
			_calcImport = new CalculatorImport();
			_calcImport.ImportsSatisfied += (sender, e) =>
			{
				ImportsSatisfied?.Invoke(this, e);
			};
		}


		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (CompositionHost host = configuration.CreateContainer())
			{
				host.SatisfyImports(_calcImport);
			}
		}

		public IEnumerable<IOperation> GetOperators() =>
			 _calcImport.Calculator.Value.GetOperations();


		public double InvokeCalculator(IOperation operation, double[] operands) =>
			 _calcImport.Calculator.Value.Operate(operation, operands);

	}
}
