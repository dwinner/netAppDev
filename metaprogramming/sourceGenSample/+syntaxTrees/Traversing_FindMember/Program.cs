using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Traversing_FindMember
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Test
{
	static void Main() => Console.WriteLine (""Hello"");
}");

         var root = tree.GetRoot();

         var node = root.DescendantNodes()
            .First(m => m.Kind() == SyntaxKind.MethodDeclaration);
         Console.WriteLine(node);

         var methodDecl = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Single();
         Console.WriteLine(methodDecl);

         var mainMethodDecl = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Single(m => m.Identifier.Text == "Main");
         Console.WriteLine(mainMethodDecl);

         var someNode = root.DescendantNodes()
            .First(m =>
               m.Kind() == SyntaxKind.MethodDeclaration &&
               m.ChildTokens().Any(t => t.Kind() == SyntaxKind.IdentifierToken && t.Text == "Main"));
         Console.WriteLine(someNode);
      }
   }
}