using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Simplification;

namespace SimplificationSample
{
   static class Program
   {
      private static readonly Solution _Solution =
         MSBuildWorkspace.Create().OpenSolutionAsync(@"..\..\..\Playground\Playground.sln").Result;

      static void Main()
      {
         var project = _Solution.Projects.Single(p => p.Name == "ConfigureAwaitTest");
         var baz = project.Documents.Single(d => d.Name == "Baz.cs");

         Console.WriteLine("Before:");
         Console.WriteLine();
         Console.WriteLine(baz.GetSyntaxTreeAsync().Result.GetText());

         Console.WriteLine();
         Console.WriteLine();

         var oldRoot = baz.GetSyntaxRootAsync().Result;
         var memberAccesses = oldRoot.DescendantNodes().OfType<CastExpressionSyntax>();
         var newRoot = oldRoot.ReplaceNodes(memberAccesses, (_, m) => m.WithAdditionalAnnotations(Simplifier.Annotation));
         var newDocument = baz.WithSyntaxRoot(newRoot);
         
         var result = Simplifier.ReduceAsync(newDocument).Result;
         Console.WriteLine("After:");
         Console.WriteLine();
         Console.WriteLine(result.GetSyntaxTreeAsync().Result.GetText());
      }
   }
}