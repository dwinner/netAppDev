using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ModifyingTrees
{
   public sealed class TransformToPublicMethodRewriter : CSharpSyntaxRewriter
   {
      public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
      {
         var visibilityTokens = GetVisibilityTokens(node);
         return visibilityTokens.Any(token => token.IsKind(SyntaxKind.PublicKeyword))
            ? node
            : Transform(node, visibilityTokens);
      }

      private static SyntaxNode Transform(MethodDeclarationSyntax node, IEnumerable<SyntaxToken> visibilityTokens)
      {
         var tokenPosition = 0;
         var newMethod = node.ReplaceTokens(visibilityTokens, (@old, @new)
            =>
         {
            tokenPosition++;
            return tokenPosition == 1
               ? SyntaxFactory.Token(@old.LeadingTrivia, SyntaxKind.PublicKeyword, @old.TrailingTrivia)
               : new SyntaxToken();
         });

         return newMethod;
      }

      private static ImmutableList<SyntaxToken> GetVisibilityTokens(SyntaxNode node)
         =>
            node.DescendantTokens(syntaxNode => true)
               .Where(
                  token =>
                     token.IsKind(SyntaxKind.PublicKeyword) || token.IsKind(SyntaxKind.PrivateKeyword) ||
                     token.IsKind(SyntaxKind.ProtectedKeyword) || token.IsKind(SyntaxKind.InternalKeyword))
               .ToImmutableList();
   }
}