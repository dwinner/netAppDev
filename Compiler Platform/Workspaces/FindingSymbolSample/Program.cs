using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.MSBuild;

namespace FindingSymbolSample
{
   static class Program
   {
      private static readonly Solution _Solution =
         MSBuildWorkspace.Create().OpenSolutionAsync(@"..\..\..\Playground\Playground.sln").Result;

      static void Main()
      {
         var project = _Solution.Projects.Single(p => p.Name == "ConfigureAwaitTest");

         // Найдем символы Bar.Foo и Bar.Qux
         var compilation = project.GetCompilationAsync().Result;
         var barType = compilation.GetTypeByMetadataName("Workspaces.Bar");
         var fooMethod = barType.GetMembers().Single(m => m.Name == "Foo");
         var quxProp = barType.GetMembers().Single(m => m.Name == "Qux");

         // Найдем вызывающих во всем решении
         Console.WriteLine("Find callers of Foo");
         Console.WriteLine();

         var callers = SymbolFinder.FindCallersAsync(fooMethod, _Solution).Result;
         foreach (var caller in callers)
         {
            Console.WriteLine(caller.CallingSymbol);
            foreach (var location in caller.Locations)
            {
               Console.WriteLine("    {0}", location);
            }
         }

         Console.WriteLine();
         Console.WriteLine();

         // Найдем все ссылки в решении
         Console.WriteLine("Find all references to Qux");
         Console.WriteLine();

         var references = SymbolFinder.FindReferencesAsync(quxProp, _Solution).Result;
         foreach (var reference in references)
         {
            Console.WriteLine(reference.Definition);
            foreach (var location in reference.Locations)
            {
               Console.WriteLine("    {0}", location.Location);
            }
         }
      }
   }
}