using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using CalculatorContract;
using CalculatorUtils;

namespace CalculatorViewModels
{
	public sealed class CalculatorExtensionsManager
	{
		private readonly CalculatorExtensionsImport _calcExtensionImport;

		public CalculatorExtensionsManager()
		{
			_calcExtensionImport = new CalculatorExtensionsImport();
			_calcExtensionImport.ImportsSatisfied += (sender, e) => { ImportsSatisfied?.Invoke(this, e); };
		}

		public event EventHandler<ImportEventArgs> ImportsSatisfied;

		public void InitializeContainer(params Type[] parts)
		{
			var configuration = new ContainerConfiguration().WithParts(parts);
			using (var host = configuration.CreateContainer())
			{
				host.SatisfyImports(_calcExtensionImport);
			}
		}

		public IEnumerable<Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute>> GetExtensionInformation() =>
			_calcExtensionImport.CalculatorExtensions.ToArray();
	}
}