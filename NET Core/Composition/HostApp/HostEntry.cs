using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using Contract;
using SimpleContractImpl;
using static System.Console;

namespace HostApp
{
   public class HostEntry
   {
      [Import]
      public ICalculator Calculator { get; set; }

      private static void Main()
      {
         var entry = new HostEntry();
         entry.Bootstrapper();
         entry.Run();
      }

      private void Run()
      {
         var operations = Calculator.GetOperations();
         var operationsDict = new SortedList<string, IOperation>();
         foreach (var item in operations)
         {
            WriteLine($"Name: {item.Name}, number operands: {item.NumberOperands}");
            operationsDict.Add(item.Name, item);
         }

         WriteLine();
         string selectedOp = null;
         do
         {
            try
            {
               Write("Operation? ");
               selectedOp = ReadLine();
               if (selectedOp.ToLower() == "exit" ||
                   !operationsDict.ContainsKey(selectedOp))
               {
                  continue;
               }

               var operation = operationsDict[selectedOp];
               var operands = new double[operation.NumberOperands];
               for (var i = 0; i < operation.NumberOperands; i++)
               {
                  Write($"\t operand {i + 1}? ");
                  var selectedOperand = ReadLine();
                  operands[i] = double.Parse(selectedOperand!);
               }

               WriteLine("calling calculator");
               var result = Calculator.Operate(operation, operands);
               WriteLine($"result: {result}");
            }
            catch (FormatException ex)
            {
               WriteLine(ex.Message);
               WriteLine();
            }
         } while (selectedOp != "exit");
      }

      private void Bootstrapper()
      {
         var configuration = new ContainerConfiguration()
            .WithPart<Calculator>();
         using var host = configuration.CreateContainer();
         host.SatisfyImports(this);
      }
   }
}