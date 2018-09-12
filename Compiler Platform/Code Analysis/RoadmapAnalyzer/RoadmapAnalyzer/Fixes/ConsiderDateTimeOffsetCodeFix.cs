using System;
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
using RoadmapAnalyzer.Properties;
using RoadmapAnalyzer.Rules;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConsiderDateTimeOffsetCodeFix)), Shared]
   public sealed class ConsiderDateTimeOffsetCodeFix : CodeFixProvider
   {
      private const string DiagnosticId = ConsiderDateTimeOffsetAnalyzer.DiagnosticId;
      private static readonly string _FixTitle = Resources.ConsiderDateTimeOffsetCodeFix_Title;

      public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

      public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         // Get the root syntax node for the current document
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

         // Get a reference to the diagnostic to fix
         var diagnostic = context.Diagnostics.First();

         // Find the syntax node on the span where there is a squiggle
         var node = root.FindNode(context.Span);

         // If the syntax node is not an IdentifierName return
         var identifier = node as IdentifierNameSyntax;
         if (identifier == null)
         {
            return;
         }

         // Register a code action that invokes the fix on the current document
         var codeAction = CodeAction.Create(_FixTitle,
            token => ReplaceDateTimeAsync(context.Document, identifier, token), _FixTitle);
         context.RegisterCodeFix(codeAction, diagnostic);
      }

      private async Task<Document> ReplaceDateTimeAsync(Document document, IdentifierNameSyntax oldIdentifier,
         CancellationToken cancellationToken)
      {
         // Get the root syntax node for the current document
         var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

         // Create a new syntax node
         var newIdentifier =
            oldIdentifier.WithIdentifier(
               SyntaxFactory.ParseToken(nameof(DateTimeOffset))
                  .WithLeadingTrivia(oldIdentifier.GetLeadingTrivia())
                  .WithTrailingTrivia(oldIdentifier.GetTrailingTrivia()));

         // Create a new root syntax node for the current document replacing
         // the syntax node that has diagnostic with a new syntax node
         var newRoot = root.ReplaceNode(oldIdentifier, newIdentifier);

         // Generate a new document
         var newDocument = document.WithSyntaxRoot(newRoot);
         return newDocument;
      }
   }
}