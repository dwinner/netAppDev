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
using Microsoft.CodeAnalysis.Formatting;
using RoadmapAnalyzer.Rules;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SingleStatementBodyCodeFix)), Shared]
   public class SingleStatementBodyCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(SingleStatementBodyAnalyzer.Id);

      public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var stmt = root.FindToken(diagnosticSpan.Start).Parent;
         var insertBlockCodeAction =
            CodeAction.Create("Insert curly braces", token => InsertBlockActionAsync(context.Document, stmt, token));
         context.RegisterCodeFix(insertBlockCodeAction, diagnostic);
      }

      private async Task<Document> InsertBlockActionAsync(Document document, SyntaxNode oldNode, CancellationToken token)
      {
         var newNode = oldNode;

         switch (oldNode.Kind())
         {
            case SyntaxKind.IfStatement:
               var ifStmt = (IfStatementSyntax) oldNode;
               newNode = ifStmt.WithStatement(Block(ifStmt.Statement));
               break;

            case SyntaxKind.ElseClause:
               var elseClause = (ElseClauseSyntax) oldNode;
               newNode = elseClause.WithStatement(Block(elseClause.Statement));
               break;

            case SyntaxKind.WhileStatement:
               var whileStmt = (WhileStatementSyntax) oldNode;
               newNode = whileStmt.WithStatement(Block(whileStmt.Statement));
               break;

            case SyntaxKind.ForStatement:
               var forStmt = (ForStatementSyntax) oldNode;
               newNode = forStmt.WithStatement(Block(forStmt.Statement));
               break;

            case SyntaxKind.ForEachStatement:
               var forEachStmt = (ForEachStatementSyntax) oldNode;
               newNode = forEachStmt.WithStatement(Block(forEachStmt.Statement));
               break;
         }

         newNode = newNode.WithAdditionalAnnotations(Formatter.Annotation);
         var oldRoot = await document.GetSyntaxRootAsync(token).ConfigureAwait(false);
         var newRoot = oldRoot.ReplaceNode(oldNode, newNode);

         return document.WithSyntaxRoot(newRoot);
      }
   }
}