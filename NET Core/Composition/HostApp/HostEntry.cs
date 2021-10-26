using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Contract;
using static System.Console;

namespace HostApp
{
   public class HostEntry
   {
      public ICalculator Calculator { get; set; }

      private static void Main()
      {
         var entry = new HostEntry();
         var currentDir = Environment.CurrentDirectory;
         entry.Bootstrap(currentDir);
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

      private void Bootstrap(string pluginPath)
      {
         var conventions = new ConventionBuilder();
         conventions.ForTypesDerivedFrom<ICalculator>().Export<ICalculator>().Shared();
         conventions.ForType<HostEntry>().ImportProperty<ICalculator>(entry => entry.Calculator);

         var configuration = new ContainerConfiguration()
            .WithDefaultConventions(conventions)
            .WithAssemblies(GetAssemblies(pluginPath));

         using var host = configuration.CreateContainer();
         host.SatisfyImports(this, conventions);
      }

      private static IEnumerable<Assembly> GetAssemblies(string pluginPath) =>
         Directory.EnumerateFiles(pluginPath, "*.dll")
            .Select(Assembly.LoadFile)
            .ToList();
   }
}