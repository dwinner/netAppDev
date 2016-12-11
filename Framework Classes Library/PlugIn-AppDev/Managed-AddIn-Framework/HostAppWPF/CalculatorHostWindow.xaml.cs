using HostView;
using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Wrox.ProCSharp.MAF.Properties;

namespace Wrox.ProCSharp.MAF
{
   public partial class CalculatorHostWindow
   {
      private Calculator _activeAddIn;
      private Operation _currentOperation;

      public CalculatorHostWindow()
      {
         InitializeComponent();
         FindAddIns();
      }

      private void FindAddIns()
      {
         try
         {
            listAddIns.DataContext = AddInStore.FindAddIns(typeof(Calculator), Settings.Default.PipelinePath);
         }
         catch (DirectoryNotFoundException)
         {
            MessageBox.Show("Verify the pipeline directory in the config file");
            Application.Current.Shutdown();
         }
      }

      private void ListOperatons()
      {
         listOperations.DataContext = _activeAddIn.GetOperations();
      }

      private void ListOperands(IEnumerable<double> operands)
      {
         listOperands.DataContext =
            operands.Select((operand, index) => new OperandUi { Index = index + 1, Value = operand }).ToArray();
      }

      private void UpdateStore(object sender, RoutedEventArgs e)
      {
         string[] messages = AddInStore.Update(Settings.Default.PipelinePath);
         if (messages.Length != 0)
         {
            MessageBox.Show(string.Join("\n", messages), "AddInStore Warnings", MessageBoxButton.OK,
               MessageBoxImage.Warning);
         }
      }

      private void RebuildStore(object sender, RoutedEventArgs e)
      {
         string[] messages = AddInStore.Rebuild(Settings.Default.PipelinePath);
         if (messages.Length != 0)
         {
            MessageBox.Show(string.Join("\n", messages), "AddInStore Warnings", MessageBoxButton.OK,
               MessageBoxImage.Warning);
         }
      }

      private void RefreshAddIns(object sender, RoutedEventArgs e)
      {
         FindAddIns();
      }

      private void App_Exit(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private void ActivateAddIn(object sender, RoutedEventArgs e)
      {
         var el = sender as FrameworkElement;

         Trace.Assert(el != null, "ActivateAddIn invoked from the wrong control type");

         var addIn = el.Tag as AddInToken;
         Trace.Assert(el.Tag != null,
            String.Format("An AddInToken must be assigned to the Tag property of the control {0}", el.Name));

         var process = new AddInProcess { KeepAlive = false };

         if (addIn != null)
            _activeAddIn = addIn.Activate<Calculator>(process, AddInSecurityLevel.Internet);

         ListOperatons();
      }

      private void OperationSelected(object sender, RoutedEventArgs e)
      {
         var el = sender as FrameworkElement;
         Trace.Assert(el != null, "OperationSelected invoked from the wrong control type");

         var op = el.Tag as Operation;
         Trace.Assert(op != null,
            String.Format("An Operation must be assigned to the Tag property of the of the control {0}", el.Name));

         _currentOperation = op;
         ListOperands(new double[op.NumberOperands]);

         buttonCalculate.IsEnabled = true;
      }

      private void Calculate(object sender, RoutedEventArgs e)
      {
         var operandsUi = (OperandUi[])listOperands.DataContext;
         double[] operands = operandsUi.Select(opui => opui.Value).ToArray();
         double result = _activeAddIn.Operate(_currentOperation, operands);
         labelResult.Content = result;
      }

      private class OperandUi
      {
         public int Index { get; set; }
         public double Value { get; set; }
      }
   }
}