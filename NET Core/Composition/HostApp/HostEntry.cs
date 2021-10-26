using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Contract;
using JetBrains.Annotations;
using static System.Console;

namespace HostApp
{
   public class HostEntry
   {
      private const string AddInTarget = "addins";

      [UsedImplicitly]
      public IEnumerable<Lazy<ICalculator>> Calculators { get; set; }

      private static void Main()
      {
         var entry = new HostEntry();
         var currentDir = Environment.CurrentDirectory;
         var addInPath = Path.Combine(currentDir, AddInTarget);
         entry.Bootstrap(addInPath);
         entry.Run();
      }

      private void Run()
      {
         WriteLine("Loaded extensions");
         var strBuilder = new StringBuilder();
         foreach (var deferredCalc in Calculators)
         {
            var calculator = deferredCalc.Value;
            strBuilder
               .Append($"AddIn name: {calculator.AddInName}. ")
               .Append($"Operation count: {calculator.GetOperations().Count}")
               .AppendLine();
         }

         WriteLine(strBuilder.ToString());
      }

      private void Bootstrap(string pluginPath)
      {
         var conventions = new ConventionBuilder();
         conventions
            .ForTypesDerivedFrom<ICalculator>()
            .Export<ICalculator>()
            .Shared();
         conventions
            .ForType<HostEntry>()
            .ImportProperty<IEnumerable<Lazy<ICalculator>>>(entry => entry.Calculators);

         var addInAssemblies = GetAssemblies(pluginPath);
         var configuration = new ContainerConfiguration()
            .WithDefaultConventions(conventions)
            .WithAssemblies(addInAssemblies);

         using var host = configuration.CreateContainer();
         host.SatisfyImports(this, conventions);
      }

      private static IEnumerable<Assembly> GetAssemblies(string pluginPath) =>
         Directory.EnumerateFiles(pluginPath, "*.dll")
            .Select(Assembly.LoadFile)
            .ToList();
   }
}