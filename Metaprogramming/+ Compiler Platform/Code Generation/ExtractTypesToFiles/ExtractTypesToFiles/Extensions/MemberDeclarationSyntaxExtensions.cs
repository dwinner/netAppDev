using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ExtractTypesToFiles.Extensions
{
   public static class MemberDeclarationSyntaxExtensions
   {
      public static CompilationUnitSyntax GetCompilationUnitForType(
         this MemberDeclarationSyntax @this,
         SemanticModel model, string containingNamespace)
         => CompilationUnit()
            .WithUsings(@this.GenerateUsingDirectives(model))
            .WithMembers(
               SingletonList<MemberDeclarationSyntax>(
                  NamespaceDeclaration(
                     IdentifierName(containingNamespace))
                     .WithMembers(
                        List(new[] {@this}))))
            .WithAdditionalAnnotations(Formatter.Annotation);
   }
}