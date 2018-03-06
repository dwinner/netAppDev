using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using RoadmapAnalyzer.Properties;
using RoadmapAnalyzer.Rules;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddAsyncSuffixCodeFix)), Shared]
   public sealed class AddAsyncSuffixCodeFix : CodeFixProvider
   {
      private const string DiagnosticId = AddAsyncSuffixAnalyzer.DiagnosticId;

      public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var declaration =
            root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().First();
         context.RegisterCodeFix(
            CodeAction.Create(
               Resources.AddAsyncSuffixCodeFix_Title,
               token => MakeMethodNameAsync(context.Document, declaration, token),
               Resources.AddAsyncSuffixCodeFix_Title),
            diagnostic);
      }

      private static async Task<Solution> MakeMethodNameAsync(
         Document document, MethodDeclarationSyntax methodDecl, CancellationToken cancellationToken)
      {
         var identifierToken = methodDecl.Identifier;
         var newName = $"{identifierToken.Text}Async";

         // Get the symbol representing the type
         var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
         var methodSymbol = semanticModel.GetDeclaredSymbol(methodDecl, cancellationToken);

         // Produce a new solution that has all references to that type renamed, including the declaration
         var originalSolution = document.Project.Solution;
         var optionSet = originalSolution.Workspace.Options;
         var newSolution =
            await
               Renamer.RenameSymbolAsync(document.Project.Solution, methodSymbol, newName, optionSet, cancellationToken)
                  .ConfigureAwait(false);

         // Return the new solution with the now-uppercase type name.
         return newSolution;
      }
   }
}