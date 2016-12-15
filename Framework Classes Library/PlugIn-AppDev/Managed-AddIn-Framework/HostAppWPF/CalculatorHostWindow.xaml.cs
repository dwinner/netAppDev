using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using HostView;

namespace Wrox.ProCSharp.MAF
{
   public partial class CalculatorHostWindow
   {
      private static readonly string _DefaultPipelinePath;
      private Calculator _activatedAddIn;
      private Operation _currentOperation;

      static CalculatorHostWindow()
      {
         var currentDirectory = Environment.CurrentDirectory;
         var currentDirInfo = new DirectoryInfo(currentDirectory);
         if (currentDirInfo.Parent != null)
         {
            if (currentDirInfo.Parent.Parent != null)
            {
               var directoryInfo = currentDirInfo.Parent.Parent.Parent;
               if (directoryInfo != null)
               {
                  _DefaultPipelinePath
                     = string.Format("{0}{1}Pipeline", directoryInfo.FullName, Path.DirectorySeparatorChar);
               }
            }
         }
         else
         {
            throw new InvalidOperationException("Failed pipeline directory path");
         }
      }

      public CalculatorHostWindow()
      {
         InitializeComponent();
         FindAddIns();
      }

      private void FindAddIns()
      {
         try
         {
            AddInListBox.DataContext = AddInStore.FindAddIns(typeof (Calculator), _DefaultPipelinePath);
         }
         catch (DirectoryNotFoundException)
         {
            MessageBox.Show("Verify the pipeline directory in the config file");
            Application.Current.Shutdown();
         }
      }

      private void ListOperations()
      {
         OperationsListBox.DataContext = _activatedAddIn.GetOperations();
      }

      private void ListOperands(IEnumerable<double> operands)
      {
         OperandListBox.DataContext =
            operands.Select((operand, index) => new OperandUi {Index = index + 1, Value = operand}).ToArray();
      }

      private void OnUpdateStore(object sender, RoutedEventArgs e)
      {
         var messages = AddInStore.Update(_DefaultPipelinePath);
         if (messages.Length != 0)
         {
            MessageBox.Show(string.Join(Environment.NewLine, messages), "AddInStore Warnings", MessageBoxButton.OK,
               MessageBoxImage.Warning);
         }
      }

      private void OnRebuildStore(object sender, RoutedEventArgs e)
      {
         var messages = AddInStore.Rebuild(_DefaultPipelinePath);
         if (messages.Length != 0)
         {
            MessageBox.Show(string.Join("\n", messages), "AddInStore Warnings", MessageBoxButton.OK,
               MessageBoxImage.Warning);
         }
      }

      private void OnRefreshAddIns(object sender, RoutedEventArgs e)
      {
         FindAddIns();
      }

      private void OnExit(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private void ActivateAddIn(object sender, RoutedEventArgs e)
      {
         var element = sender as FrameworkElement;
         Debug.Assert(element != null, "element != null");
         var addIn = element.Tag as AddInToken;         
         var process = new AddInProcess {KeepAlive = false};
         if (addIn != null)
         {
            _activatedAddIn = addIn.Activate<Calculator>(process, AddInSecurityLevel.Internet);
         }

         ListOperations();
      }

      private void OperationSelected(object sender, RoutedEventArgs e)
      {
         var element = sender as FrameworkElement;
         Debug.Assert(element != null, "element != null");
         var currentOperation = element.Tag as Operation;         
         _currentOperation = currentOperation;
         Debug.Assert(currentOperation != null, "currentOperation != null");
         ListOperands(new double[currentOperation.NumberOperands]);

         ButtonCalculate.IsEnabled = true;
      }

      private void Calculate(object sender, RoutedEventArgs e)
      {
         var operandsUi = (OperandUi[]) OperandListBox.DataContext;
         var operands = operandsUi.Select(operandUi => operandUi.Value).ToArray();
         var result = _activatedAddIn.Operate(_currentOperation, operands);
         ResultLabel.Content = result;
      }

      private class OperandUi
      {
         public int Index { get; set; }
         public double Value { get; set; }
      }
   }
}