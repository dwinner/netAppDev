using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Properties;
using CalculatorContract;

namespace Calculator
{
   public sealed class CalculatorManager : IDisposable
   {
      private readonly CalculatorViewModel _calculatorViewModel;
      private CalculatorExtensionImport _calcExtensionImport;
      private CalculatorImport _calcImport;
      private DirectoryCatalog _catalog;
      private CompositionContainer _container;

      public CalculatorManager(CalculatorViewModel calculatorViewModel)
      {
         _calculatorViewModel = calculatorViewModel;
      }

      public void Dispose()
      {
         _catalog.Dispose();
         _container.Dispose();
      }

      public async void InitializeContainer()
      {
         _catalog = new DirectoryCatalog(Settings.Default.AddInDirectory);

         #region Реакция на изменение загружаемых в каталог дополнений

         _catalog.Changed += (sender, args) =>
         {
            var stringBuilder = new StringBuilder();

            foreach (var metadata in args.AddedDefinitions.SelectMany(definition => definition.Metadata))
            {
               stringBuilder.AppendFormat("Added definition with metadata - key: {0}, value: {1}{2}", metadata.Key,
                  metadata.Value, Environment.NewLine);
            }

            foreach (var metadata in args.RemovedDefinitions.SelectMany(definition => definition.Metadata))
            {
               stringBuilder.AppendFormat("Removed definition with metadata - key: {0}, value: {1}{2}", metadata.Key,
                  metadata.Value, Environment.NewLine);
            }

            _calculatorViewModel.Status += stringBuilder.ToString();
         };

         #endregion

         _container = new CompositionContainer(_catalog);

         #region Реакция на изменение экспортируемых частей в контейнере

         _container.ExportsChanged += (sender, args) =>
         {
            var strBuilder = new StringBuilder();

            foreach (ExportDefinition addedExport in args.AddedExports)
            {
               strBuilder.AppendFormat("Added export {0}{1}", addedExport.ContractName, Environment.NewLine);
            }

            foreach (ExportDefinition removedExport in args.RemovedExports)
            {
               strBuilder.AppendFormat("Removed export {0}{1}", removedExport.ContractName, Environment.NewLine);
            }
         };

         #endregion

         _calcImport = new CalculatorImport();

         #region Реакция на принятые импорты

         _calcImport.ImportsSatisfied +=
            (sender, args) =>
            {
               _calculatorViewModel.Status += string.Format("{0}{1}", args.StatusMessage, Environment.NewLine);
            };

         #endregion

         await Task.Run(() => _container.ComposeParts());
         await InitializeOperationsAsync();
      }

      public Task InitializeOperationsAsync()
      {
         Contract.Requires(_calcImport != null);
         Contract.Requires(_calcImport.Calculator != null);

         return Task.Run(() =>
         {
            IList<IOperation> operations = _calcImport.Calculator.Value.GetOperations();
            lock (_calculatorViewModel.SyncCalcAddInOperators)
            {
               _calculatorViewModel.CalcAddInOperators.Clear();
               foreach (IOperation operation in operations) _calculatorViewModel.CalcAddInOperators.Add(operation);
            }
         });
      }

      public async Task InvokeCalculatorAsync(IOperation anOperation, double[] operands)
      {
         double result = await Task.Run(() => _calcImport.Calculator.Value.Operate(anOperation, operands));
         _calculatorViewModel.Result = result.ToString(CultureInfo.InvariantCulture);
         _calculatorViewModel.Input = string.Empty;
      }

      public void RefreshExtensions()
      {
         _catalog.Refresh();
         _calcExtensionImport = new CalculatorExtensionImport();
         _calcExtensionImport.ImportsSatisfied +=
            (sender, args) =>
            {
               _calculatorViewModel.Status += string.Format("{0}{1}", args.StatusMessage, Environment.NewLine);
            };

         _container.ComposeParts(_calcExtensionImport);
         _calculatorViewModel.CalcExtensions.Clear();
         foreach (var extension in _calcExtensionImport.CalculatorExtensions)
            _calculatorViewModel.CalcExtensions.Add(extension);
      }

      private static void GetExportInformation<T>(ExportProvider aContainer, StringBuilder aStringBuilder)
      {
         foreach (var export in aContainer.GetExports<T, IDictionary<string, object>>())
         {
            aStringBuilder.AppendFormat("Export type: {0}{1}", export.Value.GetType().Name, Environment.NewLine);
            foreach (string metaKey in export.Metadata.Keys)
            {
               aStringBuilder.AppendFormat("\tKey: {0}, Value: {1}{2}", metaKey, export.Metadata[metaKey],
                  Environment.NewLine);
            }
         }
      }

      public void ShowExports()
      {
         var builder = new StringBuilder();
         GetExportInformation<ICalculator>(_container, builder);
         GetExportInformation<ICalculatorExtension>(_container, builder);
         _calculatorViewModel.Status += builder.ToString();
      }
   }
}