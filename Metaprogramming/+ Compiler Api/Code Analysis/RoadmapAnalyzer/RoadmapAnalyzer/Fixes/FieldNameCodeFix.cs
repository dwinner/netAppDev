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
using RoadmapAnalyzer.Rules;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(FieldNameCodeFix)), Shared]
   public class FieldNameCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(FieldNameAnalyzer.Id);

      public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var declaration =
            root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<VariableDeclaratorSyntax>().First();
         var renameFieldAction = CodeAction.Create("Remove underscore",
            token => RemoveUnderscoreActionAsync(context.Document, declaration, token));
         context.RegisterCodeFix(renameFieldAction, diagnostic);
      }

      private async Task<Solution> RemoveUnderscoreActionAsync(Document document, VariableDeclaratorSyntax declaration,
         CancellationToken token)
      {
         // Узнаем имя поля
         var identifierToken = declaration.Identifier;
         var newName = identifierToken.Text.Substring(1);

         // Узнаем символ, предоставляющий тип для переименования
         var model = await document.GetSemanticModelAsync(token).ConfigureAwait(false);
         var fieldSymbol = model.GetDeclaredSymbol(declaration, token);

         // Создадим новое решение, в котором все ссылки на это поле были переименованы
         var originalSolution = document.Project.Solution;
         var optionSet = originalSolution.Workspace.Options;
         var newSolution =
            await
               Renamer.RenameSymbolAsync(document.Project.Solution, fieldSymbol, newName, optionSet, token)
                  .ConfigureAwait(false);

         // Вернем новое решение с переименованным символом
         return newSolution;
      }
   }
}