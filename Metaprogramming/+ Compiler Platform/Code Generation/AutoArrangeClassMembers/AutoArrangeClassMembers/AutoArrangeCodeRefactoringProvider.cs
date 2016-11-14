using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoArrangeClassMembers.Properties;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace AutoArrangeClassMembers
{
   [ExportCodeRefactoringProvider(
      LanguageNames.CSharp, Name = nameof(AutoArrangeCodeRefactoringProvider)), Shared]
   public sealed class AutoArrangeCodeRefactoringProvider : CodeRefactoringProvider
   {
      private static readonly IComparer<string> _NaturalSortComparer = ComparerFactory.NaturalSort;

      public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var node = root.FindNode(context.Span);
         var typeDecl = node as TypeDeclarationSyntax;
         if (typeDecl == null)
         {
            return;
         }

         var codeActionText = string.Format(Resources.AutoArrange_ActionTitle, typeDecl.Identifier.Text);
         var autoArrangeAction = CodeAction.Create(codeActionText,
            cancellationToken => AutoArrangeAsync(context.Document, typeDecl, cancellationToken));
         context.RegisterRefactoring(autoArrangeAction);
      }

      private static async Task<Document> AutoArrangeAsync(
         Document document, TypeDeclarationSyntax oldTypeDecl, CancellationToken cancellationToken)
      {         
         var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
         var arrangeWalker = new AutoArrangeWalker();
         arrangeWalker.Visit(oldTypeDecl);

         var propertyDeclarations = arrangeWalker.PropertyDeclarations;
         if (propertyDeclarations.Count >= 2)
         {
            propertyDeclarations.Sort(
               (firstProperty, secondProperty) =>
                  _NaturalSortComparer.Compare(firstProperty.Identifier.Text, secondProperty.Identifier.Text));            
            var sortedProperties = propertyDeclarations.Skip(1).ToArray();
            var newTypeDecl = oldTypeDecl.RemoveNodes(sortedProperties, SyntaxRemoveOptions.AddElasticMarker);
            newTypeDecl = newTypeDecl.InsertNodesAfter(newTypeDecl.ChildNodes().First(), sortedProperties);
            var newRoot = oldRoot
               .ReplaceNode(oldTypeDecl, newTypeDecl)
               .WithAdditionalAnnotations(Formatter.Annotation);

            return document.WithSyntaxRoot(newRoot);
         }

         return document.WithSyntaxRoot(oldRoot);
      }
   }
}