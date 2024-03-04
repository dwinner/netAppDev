using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Transforming_CreatingNodes
{
   internal static class Program
   {
      private static void Main()
      {
         var qualifiedName = SyntaxFactory.QualifiedName(
            SyntaxFactory.IdentifierName("System"),
            SyntaxFactory.IdentifierName("Text"));

         var usingDirective =
            SyntaxFactory.UsingDirective(qualifiedName);

         Console.WriteLine(usingDirective.ToFullString()); // usingSystem.Text;

         // Explicitly adding trivia:
         usingDirective = usingDirective.WithUsingKeyword(
            usingDirective.UsingKeyword.WithTrailingTrivia(
               SyntaxFactory.Whitespace(" ")));

         Console.WriteLine(usingDirective.ToFullString()); // using System.Text;

         var existingTree = CSharpSyntaxTree.ParseText("class Program {}");
         var existingUnit = (CompilationUnitSyntax)existingTree.GetRoot();

         var unitWithUsing = existingUnit.AddUsings(usingDirective);

         var treeWithUsing = CSharpSyntaxTree.Create(unitWithUsing.NormalizeWhitespace());
         Console.WriteLine(treeWithUsing.ToString());
      }
   }
}