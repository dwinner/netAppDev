using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExtractTypesToFiles.Extensions
{
   public static class SyntaxNodeExtensions
   {
      internal static ImmutableArray<TypeToRemove> GetTypesToRemove(
         this SyntaxNode @this, SemanticModel model, string documentFileNameWithoutExtension)
         =>
            (from typeNode in @this.DescendantNodes(node => true).OfType<TypeDeclarationSyntax>()
               let type = model.GetDeclaredSymbol(typeNode) as ITypeSymbol
               where type.ContainingType == null
                     && type.Name != documentFileNameWithoutExtension
               select new TypeToRemove(typeNode, type)).ToImmutableArray();

      public static SyntaxList<UsingDirectiveSyntax> GenerateUsingDirectives(
         this SyntaxNode @this, SemanticModel model)
      {
         var namespacesForType = new SortedSet<string>();

         foreach (
            var containingNamespace in
               @this.DescendantNodes(node => true)
                  .Select(node => model.GetSymbolInfo(node).Symbol)
                  .Where(
                     symbol =>
                        symbol != null
                           && symbol.Kind != SymbolKind.Namespace
                           && symbol.ContainingNamespace != null
                           && (symbol as ITypeSymbol)?.SpecialType != SpecialType.System_Void)
                  .Select(symbol => symbol.GetContainingNamespace())
                  .Where(containingNamespace => !string.IsNullOrWhiteSpace(containingNamespace)))
         {
            namespacesForType.Add(containingNamespace);
         }

         return SyntaxFactory.List(
            namespacesForType.Select(
               @namespace => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(@namespace))));
      }
   }
}