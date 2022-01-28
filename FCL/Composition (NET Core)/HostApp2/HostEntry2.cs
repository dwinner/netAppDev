using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Contract;
using static System.Console;

namespace HostApp2
{
   internal static class HostEntry2
   {
      private const string AddInTarget = "addins";
      private static IEnumerable<ICalculator> _calcPlugins;

      [ModuleInitializer]
      internal static void Initializer()
      {
         var conventions = new ConventionBuilder();
         conventions
            .ForTypesDerivedFrom<ICalculator>()
            .Export<ICalculator>()
            .Shared();

         var addInPath = Path.Combine(Environment.CurrentDirectory, AddInTarget);
         var configuration = new ContainerConfiguration()
            .WithAssembliesInPath(addInPath, conventions);

         using var container = configuration.CreateContainer();
         _calcPlugins = container.GetExports<ICalculator>();
      }

      private static void Main(string[] args)
      {
         WriteLine("Loaded extensions");
         var strBuilder = new StringBuilder();
         foreach (var calculator in _calcPlugins)
         {
            strBuilder
               .Append($"AddIn name: {calculator.AddInName}. ")
               .Append($"Operation count: {calculator.GetOperations().Count}")
               .AppendLine();
         }

         WriteLine(strBuilder.ToString());
      }
   }
}