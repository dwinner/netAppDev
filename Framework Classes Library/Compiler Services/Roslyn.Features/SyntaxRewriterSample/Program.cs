// Простой пример перезаписи дерева

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxRewriterSample
{
   internal static class Program
   {
      private static readonly SyntaxTree SourceTree = CSharpSyntaxTree.ParseText(@"
public class Sample
{
   public void Foo()
   {
      Console.WriteLine();
      #region SomeRegion
      //Some other code
      #endregion
      ;
   }
}");

      private static void Main()
      {
         CSharpSyntaxRewriter syntaxRewriter = new EmptyStatementRemoval();
         var result = syntaxRewriter.Visit(SourceTree.GetRoot());
         Console.WriteLine(result.ToFullString());
      }
   }

   public sealed class EmptyStatementRemoval : CSharpSyntaxRewriter  // Удаление пустых операторов
   {
      public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
      {
         // Строим пустой оператор с пропущенной ;
         return
            node.WithSemicolonToken(
               SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken)
                  .WithLeadingTrivia(node.SemicolonToken.LeadingTrivia)
                  .WithTrailingTrivia(node.SemicolonToken.TrailingTrivia));
      }
   }
}