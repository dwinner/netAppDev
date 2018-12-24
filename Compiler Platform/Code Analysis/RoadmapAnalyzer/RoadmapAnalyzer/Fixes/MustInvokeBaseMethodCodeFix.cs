using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using RoadmapAnalyzer.Rules;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.RefKind;

namespace RoadmapAnalyzer.Fixes
{
   [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MustInvokeBaseMethodCodeFix))]
   [Shared]
   public sealed class MustInvokeBaseMethodCodeFix : CodeFixProvider
   {
      public const string CodeFixDescription = "Add base invocation";

      public override ImmutableArray<string> FixableDiagnosticIds
         => ImmutableArray.Create(MustInvokeBaseMethodAnalyzer.DiagnosticId);

      public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

      public override async Task RegisterCodeFixesAsync(CodeFixContext context)
      {
         var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
         context.CancellationToken.ThrowIfCancellationRequested();

         var diagnostic = context.Diagnostics[0];
         var methodNode = root.FindNode(diagnostic.Location.SourceSpan) as MethodDeclarationSyntax;

         var model = await context.Document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false);
         var methodSymbol = model.GetDeclaredSymbol(methodNode);

         #region Syntax tree way

         //var invocation = CreateInvocation(methodSymbol);
         //invocation = AddArguments(methodSymbol, invocation);
         //var statement = CreateStatement(methodNode,methodSymbol,invocation);

         #endregion

         #region Code parsing way

         var statement = CreateStatement(methodNode, methodSymbol);
         var newRoot = CreateNewRoot(root, methodNode, statement);

         #endregion

         context.RegisterCodeFix(
            CodeAction.Create(CodeFixDescription, token => Task.FromResult(context.Document.WithSyntaxRoot(newRoot)),
               CodeFixDescription), diagnostic);
      }

      private static string CreateSafeLocalVariableName(SyntaxNode methodNode, ISymbol methodSymbol)
      {
         var localDeclarations = methodNode.DescendantNodes(_ => true).OfType<VariableDeclaratorSyntax>();
         var returnValueName = $"{methodSymbol.Name.Substring(0, 1).ToLower()}{methodSymbol.Name.Substring(1)}Result";
         var returnValueSafeName = returnValueName;
         var returnValueCount = 0;

         // Create a "safe" local variable name.
         var lDecls = localDeclarations as VariableDeclaratorSyntax[] ?? localDeclarations.ToArray();
         while (lDecls.Any(declaratorSyntax => declaratorSyntax.Identifier.Text == returnValueSafeName))
         {
            returnValueSafeName = $"{returnValueName}{returnValueCount}";
            returnValueCount++;
         }

         return returnValueSafeName;
      }

      private static SyntaxNode CreateNewRoot(
         SyntaxNode root, BaseMethodDeclarationSyntax methodNode, StatementSyntax statement)
      {
         var body = methodNode?.Body;
         var firstNode = body?.ChildNodes()?.FirstOrDefault();
         var newBody = firstNode != null
            ? body.InsertNodesBefore(firstNode, new[] {statement})
            : Block(statement);
         var newRoot = root.ReplaceNode(body, newBody);

         return newRoot;
      }

      private static StatementSyntax CreateStatement(SyntaxNode methodNode, IMethodSymbol methodSymbol)
      {
         var methodParameters = methodSymbol.Parameters;
         var arguments = new string[methodParameters.Length];

         for (var argIndex = 0; argIndex < methodParameters.Length; argIndex++)
         {
            var parameter = methodParameters[argIndex];
            var argument = parameter.Name;

            if (parameter.RefKind.HasFlag(Ref))
            {
               argument = $"ref {argument}";
            }
            else if (parameter.RefKind.HasFlag(Out))
            {
               argument = $"out {argument}";
            }

            arguments[argIndex] = argument;
         }

         var methodCall = $"base.{methodSymbol.Name}({string.Join(", ", arguments)});{Environment.NewLine}";
         if (!methodSymbol.ReturnsVoid)
         {
            var variableName = CreateSafeLocalVariableName(methodNode, methodSymbol);
            methodCall = $"var {variableName} = {methodCall}";
         }

         return ParseStatement(methodCall).WithAdditionalAnnotations(Formatter.Annotation);
      }


      /*
      private static InvocationExpressionSyntax CreateInvocation(ISymbol methodSymbol)
         => InvocationExpression(
            MemberAccessExpression(
               SimpleMemberAccessExpression,
               BaseExpression().WithToken(Token(BaseKeyword)),
               IdentifierName(methodSymbol.Name))
               .WithOperatorToken(Token(DotToken)));

      private static StatementSyntax CreateStatement(
         SyntaxNode methodNode, IMethodSymbol methodSymbol, ExpressionSyntax invocation)
         => (!methodSymbol.ReturnsVoid
            ? (StatementSyntax)LocalDeclarationStatement(
               VariableDeclaration(IdentifierName("var"))
                  .WithVariables(
                     SingletonSeparatedList(
                        VariableDeclarator(
                           Identifier(CreateSafeLocalVariableName(methodNode, methodSymbol)))
                           .WithInitializer(EqualsValueClause(invocation)))))
            : ExpressionStatement(invocation)).WithAdditionalAnnotations(Formatter.Annotation);

      private static InvocationExpressionSyntax AddArguments(
         IMethodSymbol methodSymbol, InvocationExpressionSyntax invocation)
      {
         var argumentCount = methodSymbol.Parameters.Length;
         if (argumentCount <= 0)
         {
            return invocation;
         }

         // Define an argument list
         var arguments = new SyntaxNodeOrToken[2 * argumentCount - 1];
         for (var argIndex = 0; argIndex < argumentCount; argIndex++)
         {
            var parameter = methodSymbol.Parameters[argIndex];
            var argument = Argument(IdentifierName(parameter.Name));

            if (parameter.RefKind.HasFlag(Ref))
            {
               argument = argument.WithRefOrOutKeyword(Token(OutKeyword));
            }
            else if (parameter.RefKind.HasFlag(Out))
            {
               argument = argument.WithRefOrOutKeyword(Token(OutKeyword));
            }

            arguments[2 * argIndex] = argument;

            if (argIndex < argumentCount - 1)
            {
               arguments[2 * argIndex + 1] = Token(CommaToken);
            }
         }

         invocation =
            invocation.WithArgumentList(
               ArgumentList(SeparatedList<ArgumentSyntax>(arguments))
                  .WithOpenParenToken(Token(OpenParenToken))
                  .WithCloseParenToken(Token(CloseParenToken)));

         return invocation;
      }
      */
   }
}