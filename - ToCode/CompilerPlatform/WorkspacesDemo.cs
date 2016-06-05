using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Simplification;
using System;
using System.IO;
using System.Linq;

namespace Workspaces
{
    class Program
    {        
        //
        // DEMO 7
        //

        static void Simplification(Solution sln)
        {
            //
            // Get the Tests\Baz.cs document.
            //

            var proj = sln.Projects.Single(p => p.Name == "Tests");
            var baz = proj.Documents.Single(d => d.Name == "Baz.cs");

            Console.WriteLine("Before:");
            Console.WriteLine();
            Console.WriteLine(baz.GetSyntaxTreeAsync().Result.GetText());

            Console.WriteLine();
            Console.WriteLine();

            var oldRoot = baz.GetSyntaxRootAsync().Result;


            //
            // Annotate nodes to be simplified with the Simplifier.Annotation.
            //

            //var newRoot = oldRoot.WithAdditionalAnnotations(Simplifier.Annotation);

            //var memberAccesses = oldRoot.DescendantNodes().OfType<MemberAccessExpressionSyntax>();
            //var newRoot = oldRoot.ReplaceNodes(memberAccesses, (_, m) => m.WithAdditionalAnnotations(Simplifier.Annotation));

            var memberAccesses = oldRoot.DescendantNodes().OfType<CastExpressionSyntax>();
            var newRoot = oldRoot.ReplaceNodes(memberAccesses, (_, m) => m.WithAdditionalAnnotations(Simplifier.Annotation));

            var newDoc = baz.WithSyntaxRoot(newRoot);


            //
            // Invoke the simplifier and print the result.
            //

            var res = Simplifier.ReduceAsync(newDoc).Result;

            Console.WriteLine("After:");
            Console.WriteLine();
            Console.WriteLine(res.GetSyntaxTreeAsync().Result.GetText());
            Console.WriteLine();
        }
    }
}
