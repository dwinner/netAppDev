using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;
using CalculatorContract;

namespace Calculator
{
   public partial class MainWindow
   {
      private readonly CalculatorManager _containerManager;
      private readonly CalculatorViewModel _viewModel = new CalculatorViewModel();
      private double[] _currentOperands;
      private IOperation _currentOperation;

      public MainWindow()
      {
         InitializeComponent();

         DataContext = _viewModel;
         _containerManager = new CalculatorManager(_viewModel);
         _containerManager.InitializeContainer();

         BindingOperations.EnableCollectionSynchronization(
            _viewModel.CalcAddInOperators, _viewModel.SyncCalcAddInOperators);
         BindingOperations.EnableCollectionSynchronization(
            _viewModel.ActivatedExtensions, _viewModel.SyncActivatedExtensions);
      }

      private void OnClose(object sender, ExecutedRoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private async void OnCalculate(object sender, ExecutedRoutedEventArgs e)
      {
         if (_currentOperands.Length == 2)
         {
            var input = _viewModel.Input.Split(' ');
            _currentOperands[1] = double.Parse(input[2]);
            await _containerManager.InvokeCalculatorAsync(_currentOperation, _currentOperands).ConfigureAwait(true);
         }
      }

      private void OnRefreshExtensions(object sender, ExecutedRoutedEventArgs e)
      {
         _containerManager.RefreshExtensions();
      }

      private void OnShowExports(object sender, ExecutedRoutedEventArgs e)
      {
         _containerManager.ShowExports();
      }

      private void OnClearLog(object sender, ExecutedRoutedEventArgs e)
      {
         _viewModel.Status = string.Empty;
      }

      private void OnCloseExtension(object sender, ExecutedRoutedEventArgs e)
      {
         var originalSrcButton = e.OriginalSource as Button;
         if (originalSrcButton != null)
         {
            var extension = originalSrcButton.Tag as Lazy<ICalculatorExtension>;
            if (extension != null)
            {
               _viewModel.ActivatedExtensions.Remove(extension);
            }
         }
      }

      private void OnActivateExtension(object sender, ExecutedRoutedEventArgs e)
      {
         var originalSourceButton = e.OriginalSource as RibbonButton;
         if (originalSourceButton != null)
         {
            var control = originalSourceButton.Tag as Lazy<ICalculatorExtension>;
            if (control != null)
            {
               _viewModel.ActivatedExtensions.Add(control);
            }
         }
      }

      private void OnNumberClick(object sender, RoutedEventArgs e)
      {
         var srcButton = e.Source as Button;
         if (srcButton != null)
         {
            _viewModel.Input += srcButton.Content.ToString();
         }
      }

      private void DefineOperation(object sender, RoutedEventArgs e)
      {
         var sourceButton = e.Source as Button;
         if (sourceButton != null)
         {
            try
            {
               var operation = sourceButton.Tag as IOperation;
               if (operation != null)
               {
                  _currentOperands = new double[operation.NumberOperands];
                  _currentOperands[0] = double.Parse(_viewModel.Input);
                  _viewModel.Input += string.Format(" {0} ", operation.Name);
                  _currentOperation = operation;
               }
            }
            catch (FormatException formatEx)
            {
               _viewModel.Status = formatEx.Message;
            }
         }
      }
   }
}