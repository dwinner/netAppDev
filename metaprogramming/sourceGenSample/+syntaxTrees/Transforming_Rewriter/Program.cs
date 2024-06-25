using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Transforming_Rewriter
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program
{
	static void Main() { Test(); }
	static void Test() {         }
}");

         var rewriter = new MyRewriter();
         var newRoot = rewriter.Visit(tree.GetRoot());
         Console.WriteLine(newRoot.ToFullString());
      }
   }

   internal class MyRewriter : CSharpSyntaxRewriter
   {
      public override SyntaxNode VisitMethodDeclaration
         (MethodDeclarationSyntax node) =>
         // "Replace" the method’s identifier with an uppercase version:
         node.WithIdentifier(
            SyntaxFactory.Identifier(
               node.Identifier.LeadingTrivia, // Preserve old trivia
               node.Identifier.Text.ToUpperInvariant(),
               node.Identifier.TrailingTrivia)); // Preserve old trivia
   }
}