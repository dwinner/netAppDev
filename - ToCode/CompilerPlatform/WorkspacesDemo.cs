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
        // DEMO 6
        //

        static void Rename(Workspace ws, Solution sln)
        {
            //
            // Get Tests\Bar.cs before making changes.
            //

            var oldProj = sln.Projects.Single(p => p.Name == "Tests");
            var oldDoc = oldProj.Documents.Single(d => d.Name == "Bar.cs");

            Console.WriteLine("Before:");
            Console.WriteLine();

            var oldTxt = oldDoc.GetTextAsync().Result;
            Console.WriteLine(oldTxt);

            Console.WriteLine();
            Console.WriteLine();


            //
            // Get the symbol for the Bar.Foo method.
            //

            var comp = oldProj.GetCompilationAsync().Result;

            var barType = comp.GetTypeByMetadataName("Workspaces.Bar");
            var fooMethod = barType.GetMembers().Single(m => m.Name == "Foo");


            //
            // Perform the rename.
            //

            var newSln = Renamer.RenameSymbolAsync(sln, fooMethod, "Foo2", ws.Options).Result;


            //
            // Get Tests\Bar.cs after making changes.
            //

            var newProj = newSln.Projects.Single(p => p.Name == "Tests");
            var newDoc = newProj.Documents.Single(d => d.Name == "Bar.cs");

            Console.WriteLine("After:");
            Console.WriteLine();

            var newTxt = newDoc.GetTextAsync().Result;
            Console.WriteLine(newTxt);
        }

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
