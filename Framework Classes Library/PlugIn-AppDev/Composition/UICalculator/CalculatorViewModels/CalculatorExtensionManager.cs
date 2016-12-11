using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public sealed class CalculatorExtensionManager
	{
		private readonly CalculatorExtensionsImport _calculatorExtensionsImport;

		public CalculatorExtensionManager()
		{
			_calculatorExtensionsImport = new CalculatorExtensionsImport();
			_calculatorExtensionsImport.ImportsSatisfied += (sender, args) => OnImportsSatisfied(args);
		}

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(_calculatorExtensionsImport);
			}
		}

		public IEnumerable<Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute>> GetExtensionInformation()
			=> _calculatorExtensionsImport.CalculatorExtensions.ToArray();

		private void OnImportsSatisfied(ImportEventArgs e) => ImportsSatisfied?.Invoke(this, e);
	}
}