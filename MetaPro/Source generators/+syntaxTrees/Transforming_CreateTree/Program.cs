using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Transforming_CreateTree
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

         var unit = SyntaxFactory.CompilationUnit().AddUsings(usingDirective);

         // Create a simple empty class definition:
         unit = unit.AddMembers(SyntaxFactory.ClassDeclaration("Program"));

         var tree = CSharpSyntaxTree.Create(unit.NormalizeWhitespace());
         Console.WriteLine(tree.ToString());
      }
   }
}