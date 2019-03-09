using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class CodeGeneration
   {
      private const string DefaultIndentation = "    ";

      public static PropertyDeclarationSyntax BuildOverridableProperty(IPropertySymbol propertySymbol,
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

      public static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(string indentation,
         SyntaxTokenList setAccessModifiers, string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.SetAccessorDeclaration
               )
               .WithModifiers(
                  setAccessModifiers
               )
               .WithKeyword(
                  Token(
                     TriviaList(Whitespace($"{indentation}")),
                     SyntaxKind.SetKeyword,// TODO: Replace by wrapping trivia
                     TriviaList(Space)
                  )
               )
               .WithExpressionBody(
                  ArrowExpressionClause(
                        AssignmentExpression(
                              SyntaxKind.SimpleAssignmentExpression,
                              MemberAccessExpression(
                                 SyntaxKind.SimpleMemberAccessExpression,
                                 BaseExpression(),
                                 IdentifierName(
                                    Identifier(
                                       TriviaList(),
                                       propertyName,// TODO: Replace by wrapping trivia
                                       TriviaList(Space)
                                    )
                                 )
                              ),
                              IdentifierName("value")
                           )
                           .WithOperatorToken(
                              Token(
                                 TriviaList(),
                                 SyntaxKind.EqualsToken,// TODO: Replace by wrapping trivia
                                 TriviaList(Space)
                              )
                           )
                     )
                     .WithArrowToken(
                        Token(
                           TriviaList(),
                           SyntaxKind.EqualsGreaterThanToken,// TODO: Replace by wrapping trivia
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,// TODO: Replace by wrapping trivia
                     TriviaList(LineFeed)
                  )
               );

      public static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(SyntaxTokenList getAccessModifiers,
         string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.GetAccessorDeclaration
               )
               .WithModifiers(
                  getAccessModifiers
               )
               .WithKeyword(
                  Token(
                     TriviaList(),
                     SyntaxKind.GetKeyword,// TODO: Replace by wrapping trivia
                     TriviaList(Space)
                  )
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
                        Token(
                           TriviaList(),
                           SyntaxKind.EqualsGreaterThanToken,// TODO: Replace by wrapping trivia
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,// TODO: Replace by wrapping trivia
                     TriviaList(LineFeed)
                  )
               );

      public static MethodDeclarationSyntax BuildOverridableMethod(IMethodSymbol methodSymbol,
         string indentation = DefaultIndentation)
      {
         var methodName = methodSymbol.Name;
         var returnType = methodSymbol.ReturnType.SimplifyTypeName();

         var returnsByRef = methodSymbol.ReturnsByRef;
         var returnsByRefReadonly = methodSymbol.ReturnsByRefReadonly;

         var returnTypeIdentifier = IdentifierName(
            Identifier(
               TriviaList(),
               returnType,// TODO: Replace by wrapping trivia
               TriviaList(Space)
            )
         );

         var methodIdentifier = Identifier(methodName);
         var methodDecl = !returnsByRefReadonly
            ? returnsByRef
               ? MethodDeclaration(RefType(returnTypeIdentifier)
                  .WithRefKeyword(
                     SyntaxKind.RefKeyword.BuildToken(
                        Array.Empty<SyntaxTrivia>(), new[] { Space })), methodIdentifier)
               : MethodDeclaration(returnTypeIdentifier, methodIdentifier)
            : MethodDeclaration(
               RefType(returnTypeIdentifier)
                  .WithRefKeyword(
                     SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }))
                  .WithReadOnlyKeyword(
                     SyntaxKind.ReadOnlyKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space })),
               methodIdentifier);

         var methodModifiers = methodSymbol.GetModifierTokens(indentation);
         methodDecl = methodDecl.WithModifiers(methodModifiers);

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;
            if (typeParameters.Length > 0)
            {
               Func<string, CSharpSyntaxNode> genericParamFunc = genericName => TypeParameter(Identifier(genericName));
               var typeParameterList = BuildGenericParameters(typeParameters, genericParamFunc);
               var genericParametersSyntax = TypeParameterList(SeparatedList<TypeParameterSyntax>(typeParameterList));
               methodDecl = methodDecl.WithTypeParameterList(genericParametersSyntax);
            }
         }

         var parameters = methodSymbol.Parameters;
         var parameterListSyntax = parameters.Length > 0
            ? ParameterList(SeparatedList<ParameterSyntax>(GetParameterNodes(parameters)))
            : ParameterList();

         parameterListSyntax = parameterListSyntax
            .WithCloseParenToken(SyntaxKind.CloseParenToken.BuildToken(
               Array.Empty<SyntaxTrivia>(), new[] { LineFeed }));
         methodDecl = methodDecl.WithParameterList(parameterListSyntax);

         #region Build method body         

         var baseExpr = BaseExpression();
         if (methodSymbol.ReturnsVoid)
         {
            baseExpr = baseExpr
               .WithToken(SyntaxKind.BaseKeyword.BuildToken(new[] {Whitespace(indentation)},
                  Array.Empty<SyntaxTrivia>()));
         }

         InvocationExpressionSyntax invocationExpr = null;         

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;
            if (typeParameters.Length > 0)
            {               
               Func<string, CSharpSyntaxNode> genericArgFunc = IdentifierName;
               var typeParameterList = BuildGenericParameters(typeParameters, genericArgFunc);
               var typeArgumentList = TypeArgumentList(SeparatedList<TypeSyntax>(typeParameterList));
               invocationExpr = InvocationExpression(
                  MemberAccessExpression(
                     SyntaxKind.SimpleMemberAccessExpression,
                     baseExpr,
                     GenericName(Identifier(methodName))
                        .WithTypeArgumentList(typeArgumentList)));
            }
            else
            {
               invocationExpr = InvocationExpression(
                  MemberAccessExpression(
                     SyntaxKind.SimpleMemberAccessExpression,
                     baseExpr,
                     GenericName(Identifier(methodName))));
            }
         }
         else
         {
            invocationExpr = InvocationExpression(
               MemberAccessExpression(
                  SyntaxKind.SimpleMemberAccessExpression,
                  baseExpr,
                  IdentifierName(methodName)));
         }

         if (parameters.Length > 0)
         {
            var argumentNodes = new List<SyntaxNodeOrToken>(parameters.Length * 2);
            for (int argIndex = 0; argIndex < parameters.Length; argIndex++)
            {
               var parameterSymbol = parameters[argIndex];
               var argumentName = parameterSymbol.Name;
               var argumentSyntax = Argument(IdentifierName(argumentName));

               switch (parameterSymbol.RefKind)
               {
                  case RefKind.None:
                     break;

                  case RefKind.Ref:
                     argumentSyntax =
                        argumentSyntax.WithRefKindKeyword(
                           SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }));
                     break;

                  case RefKind.Out:
                     argumentSyntax =
                        argumentSyntax.WithRefKindKeyword(
                           SyntaxKind.OutKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }));
                     break;

                  case RefKind.In:
                     break;

                  default:
                     throw new ArgumentOutOfRangeException();
               }

               argumentNodes.Add(argumentSyntax);
               if (argIndex != parameters.Length - 1)
               {
                  argumentNodes.Add(SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }));
               }
            }

            invocationExpr =
               invocationExpr.WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(argumentNodes)));
         }

         StatementSyntax singleStatement;
         if (methodSymbol.ReturnsVoid)
         {
            ExpressionStatementSyntax exprStmt = ExpressionStatement(invocationExpr);
            exprStmt = exprStmt.WithSemicolonToken(
               SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { LineFeed }));

            singleStatement = exprStmt;
         }
         else
         {
            ReturnStatementSyntax returnStmt = returnsByRefReadonly || returnsByRef
               ? ReturnStatement(RefExpression(invocationExpr)
                  .WithRefKeyword(SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})))
               : ReturnStatement(invocationExpr);

            returnStmt = returnStmt
               .WithReturnKeyword(
                  SyntaxKind.ReturnKeyword.BuildToken(new[] { Whitespace(indentation) }, new[] { Space }));
            returnStmt = returnStmt
               .WithSemicolonToken(
                  SyntaxKind.SemicolonToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { LineFeed }));

            singleStatement = returnStmt;
         }

         var block = Block(SingletonList<StatementSyntax>(singleStatement));
         var reducedIndentation = " ".Repeat(indentation.Length / 2);
         block = block.WithOpenBraceToken(
            SyntaxKind.OpenBraceToken.BuildToken(new[] {Whitespace(reducedIndentation)},
               new[] {LineFeed}));
         block = block.WithCloseBraceToken(
            SyntaxKind.CloseBraceToken.BuildToken(new[] {Whitespace(reducedIndentation)}, new[] {LineFeed}));

         methodDecl = methodDecl.WithBody(block);

         #endregion

         return methodDecl;
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
                  SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }));
            }
         }

         return parameterNodes;
      }

      private static IEnumerable<SyntaxNodeOrToken> BuildGenericParameters(ImmutableArray<ITypeParameterSymbol> typeParameters,
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
                  SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] { Space }));
            }
         }

         return typeParameterList;
      }      
   }
}