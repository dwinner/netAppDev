using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace AutoPropertyRewriterSample
{
   public class AutoPropertyRewriter : CSharpSyntaxRewriter
   {
      private readonly List<string> _fieldToRemove = new List<string>();
      private readonly SemanticModel _semanticModel;

      public AutoPropertyRewriter(SemanticModel semanticModel)
      {
         _semanticModel = semanticModel;
      }

      public IEnumerable<string> FieldsToRemove => _fieldToRemove;

      public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
      {
         if (HasBothAccessors(node))
         {
            var backingField =
               GetBackingFieldFromGetter(
                  node.AccessorList.Accessors.Single(
                     accessorDecl => accessorDecl.Kind() == SyntaxKind.GetAccessorDeclaration));
            var fieldDecl =
               backingField.DeclaringSyntaxReferences.First()
                  .GetSyntax()
                  .Ancestors()
                  .FirstOrDefault(a => a is FieldDeclarationSyntax) as FieldDeclarationSyntax;
            _fieldToRemove.Add(fieldDecl?.GetText().ToString());
            var propertyDecl = ConvertToAutoProperty(node).WithAdditionalAnnotations(Formatter.Annotation);

            return propertyDecl;
         }

         return node;
      }

      private static bool HasBothAccessors(BasePropertyDeclarationSyntax property)
      {
         var accessors = property.AccessorList.Accessors;
         var getter = accessors.FirstOrDefault(syntax => syntax.Kind() == SyntaxKind.GetAccessorDeclaration);
         var setter = accessors.FirstOrDefault(syntax => syntax.Kind() == SyntaxKind.SetAccessorDeclaration);

         return getter?.Body?.Statements.Count == 1 && setter?.Body?.Statements.Count == 1;
      }

      private static PropertyDeclarationSyntax ConvertToAutoProperty(PropertyDeclarationSyntax propertyDecl)
      {
         var newProperty =
            propertyDecl.WithAccessorList(
               AccessorList(
                  List(new[]
                  {
                     AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        .WithAdditionalAnnotations(Formatter.Annotation),
                     AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        .WithAdditionalAnnotations(Formatter.Annotation)
                  }))).WithAdditionalAnnotations(Formatter.Annotation);

         return newProperty;
      }

      private IFieldSymbol GetBackingFieldFromGetter(AccessorDeclarationSyntax getter)
      {
         if (getter.Body?.Statements.Count != 1)
         {
            return null;
         }

         var statement = getter.Body.Statements.Single() as ReturnStatementSyntax;
         return statement?.Expression == null
            ? null
            : _semanticModel.GetSymbolInfo(statement.Expression).Symbol as IFieldSymbol;
      }
   }
}