using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   internal static class CodeGeneration
   {
      private const string DefaultIndentation = "    ";

      internal static PropertyDeclarationSyntax BuildOverridableProperty(IPropertySymbol propertySymbol,
         string indentation = DefaultIndentation)
      {
         var propertyName = propertySymbol.Name;
         var propertyType = propertySymbol.Type.SimplifyTypeName();
         var propertyModifiers = propertySymbol.GetAccessModifiersWithOverride(indentation);
         var accessorList = propertySymbol.GetAccessorList(indentation);

         var propertyDecl = PropertyDeclaration(
               IdentifierName(propertyType.ToIdentifier()),
               propertyName.ToIdentifier())
            .WithModifiers(propertyModifiers)
            .WithAccessorList(accessorList);

         return propertyDecl;
      }

      internal static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(string indentation,
         SyntaxTokenList setAccessModifiers, string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.SetAccessorDeclaration
               )
               .WithModifiers(
                  setAccessModifiers
               )
               .WithKeyword(
                  SyntaxKind.SetKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space})
               )
               .WithExpressionBody(
                  ArrowExpressionClause(
                        AssignmentExpression(
                              SyntaxKind.SimpleAssignmentExpression,
                              MemberAccessExpression(
                                 SyntaxKind.SimpleMemberAccessExpression,
                                 BaseExpression(),
                                 IdentifierName(propertyName.ToIdentifier())
                              ),
                              IdentifierName("value")
                           )
                           .WithOperatorToken(
                              SyntaxKind.EqualsToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
                           )
                     )
                     .WithArrowToken(
                        SyntaxKind.EqualsGreaterThanToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
                     )
               )
               .WithSemicolonToken(
                  SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {LineFeed})
               );

      internal static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(SyntaxTokenList getAccessModifiers,
         string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.GetAccessorDeclaration
               )
               .WithModifiers(
                  getAccessModifiers
               )
               .WithKeyword(
                  SyntaxKind.GetKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
               )
               .WithExpressionBody(
                  ArrowExpressionClause(
                        MemberAccessExpression(
                           SyntaxKind.SimpleMemberAccessExpression,
                           BaseExpression(),
                           IdentifierName(propertyName)
                        )
                     )
                     .WithArrowToken(
                        SyntaxKind.EqualsGreaterThanToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
                     )
               )
               .WithSemicolonToken(
                  SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {LineFeed})
               );

      internal static MethodDeclarationSyntax BuildOverridableMethod(IMethodSymbol methodSymbol,
         string indentation = DefaultIndentation)
      {
         var methodName = methodSymbol.Name;
         var returnType = methodSymbol.ReturnType.SimplifyTypeName();

         var returnsByRef = methodSymbol.ReturnsByRef;
         var returnsByRefReadonly = methodSymbol.ReturnsByRefReadonly;

         // Build method declaration
         var returnTypeIdentifier = IdentifierName(returnType.ToIdentifier());
         var methodIdentifier = Identifier(methodName);
         var methodDecl = !returnsByRefReadonly
            ? returnsByRef
               ? MethodDeclaration(
                  RefType(returnTypeIdentifier)
                     .WithRefKeyword(
                        SyntaxKind.RefKeyword.BuildToken(
                           Array.Empty<SyntaxTrivia>(), new[] {Space})), methodIdentifier)
               : MethodDeclaration(returnTypeIdentifier, methodIdentifier)
            : MethodDeclaration(
               RefType(returnTypeIdentifier)
                  .WithRefKeyword(
                     SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}))
                  .WithReadOnlyKeyword(
                     SyntaxKind.ReadOnlyKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})),
               methodIdentifier);

         var methodModifiers =
            methodSymbol.GetAccessModifiersWithOverride(indentation);
         methodDecl = methodDecl.WithModifiers(methodModifiers);

         // Add type parameters if present
         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;
            if (typeParameters.Length > 0)
            {
               var typeParameterList =
                  BuildGeneric(typeParameters, genericName => TypeParameter(Identifier(genericName)));
               var genericParametersSyntax = TypeParameterList(SeparatedList<TypeParameterSyntax>(typeParameterList));
               methodDecl = methodDecl.WithTypeParameterList(genericParametersSyntax);
            }
         }

         // Build method parameters if present
         var parameters = methodSymbol.Parameters;
         var parameterListSyntax = parameters.Length > 0
            ? ParameterList(SeparatedList<ParameterSyntax>(GetParameterNodes(parameters)))
            : ParameterList();
         parameterListSyntax = parameterListSyntax
            .WithCloseParenToken(SyntaxKind.CloseParenToken.BuildToken(
               Array.Empty<SyntaxTrivia>(), new[] {LineFeed}));
         methodDecl = methodDecl.WithParameterList(parameterListSyntax);

         // Build method body         
         var invocationExpr =
            methodSymbol.BuildBaseInvocationExpr(methodSymbol.BuildBaseExpr(indentation), methodName);
         if (parameters.Length > 0)
         {
            var argumentNodes = GetArgumentNodes(parameters);
            invocationExpr =
               invocationExpr.WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(argumentNodes)));
         }

         var singleStatement =
            GetOverridableStmt(methodSymbol, indentation, invocationExpr, returnsByRefReadonly, returnsByRef);
         var block = Block(SingletonList(singleStatement));
         var reducedIndentation = " ".Repeat(indentation.Length / 2);
         block = block.WithOpenBraceToken(
            SyntaxKind.OpenBraceToken.BuildToken(new[] {Whitespace(reducedIndentation)},
               new[] {LineFeed}));
         block = block.WithCloseBraceToken(
            SyntaxKind.CloseBraceToken.BuildToken(new[] {Whitespace(reducedIndentation)}, new[] {LineFeed}));

         methodDecl = methodDecl.WithBody(block);

         return methodDecl;
      }

      private static StatementSyntax GetOverridableStmt(IMethodSymbol methodSymbol,
         string indentation,
         ExpressionSyntax invocationExpr, bool returnsByRefReadonly, bool returnsByRef)
      {
         StatementSyntax singleStatement;
         if (methodSymbol.ReturnsVoid)
         {
            var exprStmt = ExpressionStatement(invocationExpr);
            exprStmt = exprStmt.WithSemicolonToken(
               SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {LineFeed}));
            singleStatement = exprStmt;
         }
         else
         {
            var returnStmt = returnsByRefReadonly || returnsByRef
               ? ReturnStatement(RefExpression(invocationExpr)
                  .WithRefKeyword(SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})))
               : ReturnStatement(invocationExpr);

            returnStmt = returnStmt
               .WithReturnKeyword(
                  SyntaxKind.ReturnKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}));
            returnStmt = returnStmt
               .WithSemicolonToken(
                  SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {LineFeed}));

            singleStatement = returnStmt;
         }

         return singleStatement;
      }

      private static IEnumerable<SyntaxNodeOrToken> GetArgumentNodes(ImmutableArray<IParameterSymbol> parameters)
      {
         var argumentNodes = new List<SyntaxNodeOrToken>(parameters.Length * 2);
         for (var argIndex = 0; argIndex < parameters.Length; argIndex++)
         {
            var parameterSymbol = parameters[argIndex];
            var argumentName = parameterSymbol.Name;
            var argumentSyntax = Argument(IdentifierName(argumentName)).DecorateWithModifiers(parameterSymbol);
            argumentNodes.Add(argumentSyntax);
            if (argIndex != parameters.Length - 1)
            {
               argumentNodes.Add(SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         return argumentNodes;
      }

      private static IEnumerable<SyntaxNodeOrToken> GetParameterNodes(ImmutableArray<IParameterSymbol> parameters)
      {
         var parameterNodes = new List<SyntaxNodeOrToken>(parameters.Length * 2);
         for (var paramIdx = 0; paramIdx < parameters.Length; paramIdx++)
         {
            var parameterSymbol = parameters[paramIdx];
            var parameterName = parameterSymbol.Name;
            var parameterType = parameterSymbol.Type;
            var parameterSyntax = Parameter(Identifier(parameterName))
               .DecorateWithModifiers(parameterSymbol)
               .DecorateWithReturnType(parameterType, parameterSymbol);
            parameterNodes.Add(parameterSyntax);
            if (paramIdx != parameters.Length - 1)
            {
               parameterNodes.Add(
                  SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         return parameterNodes;
      }

      internal static IEnumerable<SyntaxNodeOrToken> BuildGeneric(ImmutableArray<ITypeParameterSymbol> typeParameters,
         Func<string, CSharpSyntaxNode> buildBehavior)
      {
         var typeParameterList = new List<SyntaxNodeOrToken>(typeParameters.Length * 2);
         for (var paramIdx = 0; paramIdx < typeParameters.Length; paramIdx++)
         {
            var typeParameterSymbol = typeParameters[paramIdx];
            typeParameterList.Add(buildBehavior(typeParameterSymbol.Name));
            if (paramIdx != typeParameters.Length - 1)
            {
               typeParameterList.Add(
                  SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         return typeParameterList;
      }
   }
}