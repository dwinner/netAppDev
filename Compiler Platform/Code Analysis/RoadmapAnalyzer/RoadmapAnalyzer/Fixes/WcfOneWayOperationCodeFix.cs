using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using RoadmapAnalyzer.Properties;
using RoadmapAnalyzer.Rules;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(WcfOneWayOperationCodeFix)), Shared]
   public sealed class WcfOneWayOperationCodeFix : CodeFixProvider
   {
      private const string DiagnosticId = WcfOneWayOperationAnalyzer.DiagnosticId;

      public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

      public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         var diagnostic = context.Diagnostics.First();
         var diagnosticSpan = diagnostic.Location.SourceSpan;
         var syntaxNode = root.FindNode(diagnosticSpan);
         if (syntaxNode is AttributeSyntax attributeSyntax)
         {
            var ownerMethod = attributeSyntax.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().FirstOrDefault();
            var attributeArguments = attributeSyntax.ArgumentList;
            var oneWayAttrArg =
               attributeArguments.Arguments.FirstOrDefault(
                  argument => argument.NameEquals.Name.Identifier.Text == nameof(OperationContractAttribute.IsOneWay));

            var makeVoidTitle = string.Format(Resources.WcfOneWayOperationCodeFix_MakeVoidTitle,
               ownerMethod.Identifier.Text);
            var makeIsOneWayFalseTitle = string.Format(Resources.WcfOneWayOperationCodeFix_MakeOneWayFalse_Title,
               oneWayAttrArg.NameEquals.Name.Identifier.Text);

            var makeVoidAction = CodeAction.Create(
               makeVoidTitle, token => MakeVoidAsync(context.Document, ownerMethod, token), makeVoidTitle);

            var makeOneWayFalseAction = CodeAction.Create(
               makeIsOneWayFalseTitle, token => MakeOneWayFalseAsync(context.Document, oneWayAttrArg, token),
               makeIsOneWayFalseTitle);

            context.RegisterCodeFix(makeVoidAction, diagnostic);
            context.RegisterCodeFix(makeOneWayFalseAction, diagnostic);
         }
      }

      private static async Task<Document> MakeOneWayFalseAsync(
         Document document, AttributeArgumentSyntax oneWayAttrArg, CancellationToken cancellationToken)
      {
         var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
         var falseExpr = LiteralExpression(SyntaxKind.FalseLiteralExpression);
         if (oneWayAttrArg.Expression is LiteralExpressionSyntax trueExpr)
         {
            var newRoot = oldRoot.ReplaceNode(trueExpr, falseExpr).WithAdditionalAnnotations(Formatter.Annotation);
            return document.WithSyntaxRoot(newRoot);
         }

         return document.WithSyntaxRoot(oldRoot);
      }

      private static async Task<Document> MakeVoidAsync(
         Document document, MethodDeclarationSyntax ownerMethod, CancellationToken cancellationToken)
      {
         var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
         var voidTypeSyntax = PredefinedType(Token(SyntaxKind.VoidKeyword));
         var returnType = ownerMethod.ReturnType;
         var newRoot = oldRoot.ReplaceNode(returnType, voidTypeSyntax).WithAdditionalAnnotations(Formatter.Annotation);
         return document.WithSyntaxRoot(newRoot);
      }
   }
}