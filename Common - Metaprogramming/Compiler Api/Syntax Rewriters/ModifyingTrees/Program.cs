using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ModifyingTrees
{
   internal static class Program
   {
      private const string Code = @"
using System;

public class ContainsMethods
{
	public void Method1() { }
	protected void Method2(int a, Guid b) { }
	internal void Method3(string a) { }
	private void Method4(ref string a) { }
	protected internal void Method5(long a) { }
}";

      private static void Main()
      {
         var tree = ParseSyntaxTree(Code);

         ModifyTreeViaRewriter(tree);
         WriteLine();

         ModifyTreeViaTree(tree);
         WriteLine();

         ModifyTreeViaTreeWithAnnotations(tree);
         WriteLine();
      }

      private static void ModifyTreeViaRewriter(SyntaxTree tree)
      {
         WriteLine(nameof(ModifyTreeViaRewriter));
         WriteLine(tree);
         var methodRewriter = new TransformToPublicMethodRewriter();
         var newTree = methodRewriter.Visit(tree.GetRoot());
         WriteLine(newTree);
      }

      private static void ModifyTreeViaTree(SyntaxTree tree)
      {
         WriteLine(nameof(ModifyTreeViaTree));
         WriteLine(tree);
         var methods = tree.GetRoot().DescendantNodes(node => true).OfType<MethodDeclarationSyntax>();
         var newTree = tree.GetRoot().ReplaceNodes(methods, (method, methodWithReplacement) =>
         {
            var visibilityTokens =
               method.DescendantTokens(node => true)
                  .Where(
                     token =>
                        token.IsKind(SyntaxKind.PublicKeyword) || token.IsKind(SyntaxKind.PrivateKeyword) ||
                        token.IsKind(SyntaxKind.ProtectedKeyword) || token.IsKind(SyntaxKind.InternalKeyword))
                  .ToImmutableList();
            if (visibilityTokens.Any(token => token.IsKind(SyntaxKind.PublicKeyword)))
            {
               return method;
            }

            var tokenPosition = 0;
            var newMethod = method.ReplaceTokens(visibilityTokens, (@old, @new) =>
            {
               tokenPosition++;
               return tokenPosition == 1
                  ? Token(@old.LeadingTrivia, SyntaxKind.PublicKeyword, @old.TrailingTrivia)
                  : new SyntaxToken();
            });

            return newMethod;
         });

         WriteLine(newTree);
      }

      private static void ModifyTreeViaTreeWithAnnotations(SyntaxTree tree)
      {
         const string newMethodAnnotation = "MethodMadePublic";

         WriteLine(nameof(ModifyTreeViaTreeWithAnnotations));
         WriteLine(tree);
         var methods = tree.GetRoot().DescendantNodes(_ => true).OfType<MethodDeclarationSyntax>();
         var newTree = tree.GetRoot().ReplaceNodes(methods, (method, methodWithReplacements) =>
         {
            var visibilityTokens = method.DescendantTokens(_ => true)
               .Where(_ => _.IsKind(SyntaxKind.PublicKeyword) ||
                           _.IsKind(SyntaxKind.PrivateKeyword) ||
                           _.IsKind(SyntaxKind.ProtectedKeyword) ||
                           _.IsKind(SyntaxKind.InternalKeyword)).ToImmutableList();

            if (!visibilityTokens.Any(syntaxToken => syntaxToken.IsKind(SyntaxKind.PublicKeyword)))
            {
               var tokenPosition = 0;
               var newMethod = method.ReplaceTokens(visibilityTokens,
                  (token1, token2) =>
                  {
                     tokenPosition++;
                     return tokenPosition == 1
                        ? Token(token1.LeadingTrivia, SyntaxKind.PublicKeyword, token1.TrailingTrivia)
                        : new SyntaxToken();
                  });

               return newMethod.WithAdditionalAnnotations(new SyntaxAnnotation(newMethodAnnotation));
            }

            return method;
         });

         WriteLine(newTree);
         WriteLine(
            $"Modified method count: {newTree.GetAnnotatedNodes(newMethodAnnotation).Count()}");
      }
   }
}