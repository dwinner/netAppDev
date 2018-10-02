using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoadmapAnalyzer.Properties;

namespace RoadmapAnalyzer.Refactoring
{
   [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(PublicFieldCodeRefactoringProvider)), Shared]
   internal sealed class PublicFieldCodeRefactoringProvider : CodeRefactoringProvider
   {
      public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var node = root.FindNode(context.Span);
         var fieldDecl = node as FieldDeclarationSyntax;
         if (fieldDecl != null &&
             fieldDecl.Declaration.Variables.Any(i => char.IsLower(i.Identifier.ValueText.ToCharArray().First())))
         {
            var action = CodeAction.Create(
               Resources.PublicFieldRefactoring_Title, token => RenameFieldAsync(context.Document, fieldDecl, token));
            context.RegisterRefactoring(action);
         }
      }

      private static async Task<Document> RenameFieldAsync(
         Document aDocument, FieldDeclarationSyntax aField, CancellationToken aCancellationToken)
      {
         var root = await aDocument.GetSyntaxRootAsync(aCancellationToken).ConfigureAwait(false);

         // Get a list of old variable declarations
         var oldDeclarators = aField.Declaration.Variables;

         // Store the collection of variables
         var modifiedDeclarators = new SeparatedSyntaxList<VariableDeclaratorSyntax>();
         foreach (var declarator in oldDeclarators)
         {
            var tempString = declarator.Identifier.ToFullString().Pascalize();
            modifiedDeclarators = modifiedDeclarators.Add(declarator.WithIdentifier(SyntaxFactory.ParseToken(tempString)));
         }

         var newDeclaration = aField.Declaration.WithVariables(modifiedDeclarators);
         var newField = aField.WithDeclaration(newDeclaration);
         var newRoot = root.ReplaceNode(aField, newField);
         var newDocument = aDocument.WithSyntaxRoot(newRoot);

         return newDocument;
      }
   }
}