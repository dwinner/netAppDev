using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                     SyntaxKind.SetKeyword,
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
                                       propertyName,
                                       TriviaList(Space)
                                    )
                                 )
                              ),
                              IdentifierName("value")
                           )
                           .WithOperatorToken(
                              Token(
                                 TriviaList(),
                                 SyntaxKind.EqualsToken,
                                 TriviaList(Space)
                              )
                           )
                     )
                     .WithArrowToken(
                        Token(
                           TriviaList(),
                           SyntaxKind.EqualsGreaterThanToken,
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,
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
                     SyntaxKind.GetKeyword,
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
                           SyntaxKind.EqualsGreaterThanToken,
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,
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
               returnType,
               TriviaList(Space)
            )
         );

         var methodIdentifier = Identifier(methodName);
         var methodDecl = !returnsByRefReadonly
            ? returnsByRef
               ? MethodDeclaration(RefType(returnTypeIdentifier)
                  .WithRefKeyword(
                     SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})), methodIdentifier)
               : MethodDeclaration(returnTypeIdentifier, methodIdentifier)
            : MethodDeclaration(
               RefType(returnTypeIdentifier)
                  .WithRefKeyword(
                     SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}))
                  .WithReadOnlyKeyword(
                     SyntaxKind.ReadOnlyKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})),
               methodIdentifier);

         var methodModifiers = methodSymbol.GetModifierTokens(indentation);
         methodDecl = methodDecl.WithModifiers(methodModifiers);

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;
            if (typeParameters.Length > 0)
            {
               var typeParameterList = BuildGenericParameters(typeParameters);
               var genericParametersSyntax = TypeParameterList(SeparatedList<TypeParameterSyntax>(typeParameterList));
               methodDecl = methodDecl.WithTypeParameterList(genericParametersSyntax);
            }
         }

         var parameters = methodSymbol.Parameters;
         var parameterListSyntax = parameters.Length > 0
            ? ParameterList(SeparatedList<ParameterSyntax>(GetParameterNodes(parameters)))
            : ParameterList();

         parameterListSyntax = parameterListSyntax
            .WithCloseParenToken(SyntaxKind.CloseParenToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {LineFeed}));
         methodDecl = methodDecl.WithParameterList(parameterListSyntax);

         #region Body

         // TODO: Generate method body

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
               parameterNodes.Add(SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         return parameterNodes;
      }

      private static IEnumerable<SyntaxNodeOrToken> BuildGenericParameters(
         ImmutableArray<ITypeParameterSymbol> typeParameters)
      {
         var typeParameterList = new List<SyntaxNodeOrToken>(typeParameters.Length * 2);
         for (var paramIdx = 0; paramIdx < typeParameters.Length; paramIdx++)
         {
            var typeParameterSymbol = typeParameters[paramIdx];
            typeParameterList.Add(TypeParameter(Identifier(typeParameterSymbol.Name)));
            if (paramIdx != typeParameters.Length - 1)
            {
               typeParameterList.Add(SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         return typeParameterList;
      }
   }
}