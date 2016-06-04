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
using RoadmapAnalyzer.Rules;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConfigureAwaitCodeFix)), Shared]
   public class ConfigureAwaitCodeFix : CodeFixProvider
   {
      public sealed override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(ConfigureAwaitAnalyzer.Id);

      public sealed override FixAllProvider GetFixAllProvider()
      {
         return WellKnownFixAllProviders.BatchFixer;
      }

      public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;

         // Найдем выражение await, определенное диагностикой
         //var awaitExpr =
         //   root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<PrefixUnaryExpressionSyntax>().First();

         var syntaxToken = root.FindToken(diagnosticSpan.Start);
         var awaitExpr = syntaxToken.Parent.AncestorsAndSelf().OfType<AwaitExpressionSyntax>().FirstOrDefault();         
         
         // Зарегистрируем все Code Action, который вызовет определенный Fix кода
         var firstAction = CodeAction.Create("Insert ConfigureAwait(false)",
            token => InsertConfigureAwaitAsync(context.Document, awaitExpr, false, token));
         var secondAction = CodeAction.Create("Insert ConfigureAwait(true)",
            token => InsertConfigureAwaitAsync(context.Document, awaitExpr, true, token));

         context.RegisterCodeFix(firstAction, diagnostic);
         context.RegisterCodeFix(secondAction, diagnostic);
      }

      private async Task<Document> InsertConfigureAwaitAsync(Document document, AwaitExpressionSyntax oldAwaitExpr,
         bool arg, CancellationToken token)
      {
         var configureAwait = SyntaxFactory.IdentifierName(nameof(Task.ConfigureAwait));
         var oldOperand = oldAwaitExpr.Expression;
         var newOperand = SyntaxFactory.InvocationExpression(
            SyntaxFactory.MemberAccessExpression(
               SyntaxKind.SimpleMemberAccessExpression,
               oldOperand,
               configureAwait),
            SyntaxFactory.ArgumentList(
               SyntaxFactory.Token(SyntaxKind.OpenParenToken),
               SyntaxFactory.SeparatedList(new[]
               {
                  SyntaxFactory.Argument(
                     SyntaxFactory.LiteralExpression(arg
                        ? SyntaxKind.TrueLiteralExpression
                        : SyntaxKind.FalseLiteralExpression))
               }), SyntaxFactory.Token(SyntaxKind.CloseParenToken)));

         var newAwaitExpr = oldAwaitExpr.WithExpression(newOperand)/*.WithOperand(newOperand)*/;
         var oldRoot = await document.GetSyntaxRootAsync(token).ConfigureAwait(false);
         var newRoot = oldRoot.ReplaceNode(oldAwaitExpr, newAwaitExpr);

         return document.WithSyntaxRoot(newRoot);
      }
   }
}