using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
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
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddArticleToParameterCodeFix)), Shared]
   public sealed class AddArticleToParameterCodeFix : CodeFixProvider
   {
      private const string DiagnosticId = AddArticleToParameterAnalyzer.DiagnosticId;

      public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var syntaxToken = root.FindToken(diagnosticSpan.Start);
         var parameterNode = syntaxToken.Parent.AncestorsAndSelf().OfType<ParameterSyntax>().FirstOrDefault();
         if (parameterNode != null)
         {
            var actionString = string.Format(Resources.AddArticleToParameterCodeFix_Title, syntaxToken.Text);
            context.RegisterCodeFix(
               CodeAction.Create(
                  actionString, token => AddArticleAsync(context.Document, parameterNode, token), actionString),
               diagnostic);
         }
      }

      private static async Task<Solution> AddArticleAsync(Document document, ParameterSyntax parameterNode,
         CancellationToken cancellationToken)
      {
         var parameterToken = parameterNode.Identifier;
         var paramText = parameterToken.Text;
         string newName = $"{(IsVowel(paramText[0]) ? "an" : "a")}{paramText.Pascalize()}";
         var model = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
         var parameterSymbol = model.GetDeclaredSymbol(parameterNode, cancellationToken);

         var oldSolution = document.Project.Solution;
         var optionSet = oldSolution.Workspace.Options;
         var newSolution =
            await
               Renamer.RenameSymbolAsync(oldSolution, parameterSymbol, newName, optionSet, cancellationToken)
                  .ConfigureAwait(false);

         return newSolution;
      }

      private static bool IsVowel(char aFirstChar)
      {
         char[] vowels = {'A', 'E', 'I', 'O', 'U'};
         var firstChar = char.ToUpper(aFirstChar);
         return firstChar >= 'A' && firstChar <= 'Z' && vowels.Any(vowel => firstChar == vowel);
      }
   }
}