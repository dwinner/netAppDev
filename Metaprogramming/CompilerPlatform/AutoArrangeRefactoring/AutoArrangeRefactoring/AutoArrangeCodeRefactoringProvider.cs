using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace AutoArrangeRefactoring
{
   [ExportCodeRefactoringProvider(LanguageNames.CSharp,
      Name = nameof(AutoArrangeCodeRefactoringProvider)),
      Shared]
   public sealed class AutoArrangeCodeRefactoringProvider : CodeRefactoringProvider
   {
      public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
      {         
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

         // Find the node at the selection.
         var node = root.FindNode(context.Span);

         // Only offer a refactoring if the selected node is a type declaration node.
         var typeDecl = node as TypeDeclarationSyntax;
         if (typeDecl == null)
         {
            return;
         }
                  
         var action = CodeAction.Create("Auto arrange code", cancellationToken => AutoArrangeCodeAsync(context.Document, typeDecl, cancellationToken));

         // Register auto arrange code action.
         context.RegisterRefactoring(action);
      }

      private static async Task<Solution> AutoArrangeCodeAsync(Document document, BaseTypeDeclarationSyntax typeDeclaration,
         CancellationToken cancellationToken)
      {
         AutoArrangeCaptureWalker arrangeCaptureWalker=new AutoArrangeCaptureWalker();
         arrangeCaptureWalker.Visit(typeDeclaration);
         AutoArrangeReplaceRewriter arrangeReplaceRewriter=new AutoArrangeReplaceRewriter(arrangeCaptureWalker);
         var node = arrangeReplaceRewriter.Visit(typeDeclaration);

         var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
         var newRoot = root.ReplaceNodes(new[] {typeDeclaration}, (a, b) => node);

         // Produce a reversed version of the type declaration's identifier token.
         var identifierToken = typeDeclaration.Identifier;
         var newName = new string(identifierToken.Text.ToCharArray().Reverse().ToArray());

         // Get the symbol representing the type to be renamed.
         var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
         var typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclaration, cancellationToken);

         // Produce a new solution that has all references to that type renamed, including the declaration.
         var originalSolution = document.Project.Solution;
         var optionSet = originalSolution.Workspace.Options;
         var newSolution =
            await
               Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken)
                  .ConfigureAwait(false);

         // Return the new solution with the now-uppercase type name.
         return newSolution;
      }
   }
}