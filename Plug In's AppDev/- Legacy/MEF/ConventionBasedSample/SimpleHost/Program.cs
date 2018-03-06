/**
 * Регистрация частей на основе соглашений
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using Wrox.ProCSharp.MEF.Properties;

namespace Wrox.ProCSharp.MEF
{
   public class Program
   {
      // [Import]
      public ICalculator Calculator { get; set; }

      private static void Main()
      {
         var p = new Program();
         p.Run();
      }

      public void Run()
      {
         var conventions = new RegistrationBuilder();
         conventions.ForTypesDerivedFrom<ICalculator>().Export<ICalculator>();
         conventions.ForType<Program>().ImportProperty<ICalculator>(p => p.Calculator);
         var catalog = new DirectoryCatalog(Settings.Default.AddInDirectory, conventions);
         using (CompositionService service = catalog.CreateCompositionService())
         {
            service.SatisfyImportsOnce(this, conventions);
         }

         CalculatorLoop();
      }

      public void CalculatorLoop()
      {
         IList<IOperation> operations = Calculator.GetOperations();
         var operationsDict = new SortedList<string, IOperation>();
         foreach (IOperation item in operations)
         {
            Console.WriteLine("Name: {0}, number operands: {1}", item.Name,
               item.NumberOperands);
            operationsDict.Add(item.Name, item);
         }
         Console.WriteLine();
         string selectedOp = null;
         do
         {
            try
            {
               Console.Write("Operation? ");
               selectedOp = Console.ReadLine();
               if (selectedOp != null && (selectedOp.ToLower() == "exit" || !operationsDict.ContainsKey(selectedOp)))
                  continue;
               if (selectedOp != null)
               {
                  IOperation operation = operationsDict[selectedOp];
                  var operands = new double[operation.NumberOperands];
                  for (int i = 0; i < operation.NumberOperands; i++)
                  {
                     Console.Write("\t operand {0}? ", i + 1);
                     string selectedOperand = Console.ReadLine();
                     if (selectedOperand != null) operands[i] = double.Parse(selectedOperand);
                  }
                  Console.WriteLine("calling calculator");
                  double result = Calculator.Operate(operation, operands);
                  Console.WriteLine("result: {0}", result);
               }
            }
            catch (FormatException ex)
            {
               Console.WriteLine(ex.Message);
               Console.WriteLine();
            }
         } while (selectedOp != "exit");
      }
   }
}