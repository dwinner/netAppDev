using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;

namespace RecommendSample
{
   static class Program
   {
      private static readonly Solution _Solution =
         MSBuildWorkspace.Create().OpenSolutionAsync(@"..\..\..\Playground\Playground.sln").Result;

      static void Main()
      {
         var project = _Solution.Projects.Single(p => p.Name == "ConfigureAwaitTest");
         var fooCs = project.Documents.Single(d => d.Name == "Foo.cs");

         // Find the 'dot' token in the first Console.WriteLine member access expression.
         var tree = fooCs.GetSyntaxTreeAsync().Result;
         var model = project.GetCompilationAsync().Result.GetSemanticModel(tree);
         var consoleDot = tree.GetRoot().DescendantNodes().OfType<MemberAccessExpressionSyntax>().First().OperatorToken;

         // Get recommendations at the indicated cursor position.
         if (_Solution.Workspace != null)
         {
            var result =
               Recommender.GetRecommendedSymbolsAtPositionAsync(model, consoleDot.GetLocation().SourceSpan.Start + 1,
                  _Solution.Workspace).Result;
            foreach (var symbol in result)
            {
               Console.WriteLine(symbol);
            }
         }
      }
   }
}