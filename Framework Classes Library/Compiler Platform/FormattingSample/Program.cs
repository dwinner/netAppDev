using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;

namespace FormattingSample
{
   static class Program
   {
      private static readonly Solution Solution =
         MSBuildWorkspace.Create().OpenSolutionAsync(@"..\..\..\Playground\Playground.sln").Result;

      static void Main()
      {
         var project = Solution.Projects.Single(p => p.Name == "ConfigureAwaitTest");
         var quxCs = project.Documents.Single(d => d.Name == "Qux.cs");

         Console.WriteLine("Before:");
         Console.WriteLine();
         Console.WriteLine(quxCs.GetSyntaxTreeAsync().Result.GetText());

         Console.WriteLine();
         Console.WriteLine();

         // Форматируем
         var result = Formatter.FormatAsync(quxCs).Result;

         Console.WriteLine("After:");
         Console.WriteLine();
         Console.WriteLine(result.GetSyntaxTreeAsync().Result.GetText());
         Console.WriteLine();
      }
   }
}