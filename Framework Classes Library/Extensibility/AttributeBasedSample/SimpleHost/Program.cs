/**
 * Хост-приложение для импорта дополнений
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CalculatorContract;
using SimpleHost.Properties;

namespace SimpleHost
{
   internal class Program
   {
      [Import]
      public ICalculator Calculator { get; set; }

      private static void Main()
      {
         var program = new Program();
         program.Run();
      }

      private void Run()
      {
         var catalog = new DirectoryCatalog(Settings.Default.AddInDirectory);
         var container = new CompositionContainer(catalog);
         try
         {
            container.ComposeParts(this);
         }
         catch (ChangeRejectedException changeRejectedEx)
         {
            Console.WriteLine(changeRejectedEx.Message);
            return;
         }

         IList<IOperation> operations = Calculator.GetOperations();
         var operationDict = new SortedList<string, IOperation>();
         foreach (IOperation opItem in operations)
         {
            Console.WriteLine("Name: {0}, number operands: {1}", opItem.Name, opItem.NumberOperands);
               // Вывод сведений о доступной операции
            operationDict.Add(opItem.Name, opItem);
         }
         Console.WriteLine();

         string selectedOp = null;
         do
         {
            try
            {
               Console.Write("Operation? "); // Запрос на выбор операции
               selectedOp = Console.ReadLine();
               if (selectedOp != null && (selectedOp.ToLower() == "exit" || !operationDict.ContainsKey(selectedOp)))
                  continue;

               if (selectedOp != null)
               {
                  IOperation operation = operationDict[selectedOp];
                  var operands = new double[operation.NumberOperands];
                  for (int i = 0; i < operation.NumberOperands; i++)
                  {
                     Console.Write("\t operand {0}? ", i + 1);
                     string selectedOperand = Console.ReadLine(); // Запрос на ввод операнда
                     if (selectedOperand != null) operands[i] = double.Parse(selectedOperand);
                  }

                  Console.WriteLine("Calling calculator");
                  double result = Calculator.Operate(operation, operands);
                  Console.WriteLine("Result: {0}", result);
               }
            }
            catch (FormatException formatEx)
            {
               Console.WriteLine(formatEx.Message);
               Console.WriteLine();
            }
         } while (selectedOp != "exit");
      }
   }
}