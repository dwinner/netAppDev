using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.Accessibility;

namespace GatheringOverrides
{
   public static class CodeGeneration
   {
      private const string DefaultIndentation = "    ";

      public static PropertyDeclarationSyntax BuildOverridableProperty(IPropertySymbol propertySymbol,
         string indentation = DefaultIndentation)
      {
         var propertyName = propertySymbol.Name;
         var propertyType = propertySymbol.Type.NormalizeTypeSymbol();
         var propertyModifiers = propertySymbol.GetAccessModifiersWithOverride(indentation);
         var accessorList = propertySymbol.GetAccessorList(indentation);

         var propertyDecl = PropertyDeclaration(
               IdentifierName(
                  Identifier(
                     TriviaList(),
                     propertyType,
                     TriviaList(Space)
                  )
               ),
               Identifier(
                  TriviaList(),
                  propertyName,
                  TriviaList(Space)
               )
            )
            .WithModifiers(propertyModifiers)
            .WithAccessorList(accessorList);

         return propertyDecl;
      }

      private static AccessorListSyntax GetAccessorList(this IPropertySymbol propertySymbol, string indentation)
      {
         var propertyName = propertySymbol.Name;
         AccessorListSyntax accessorList;
         var propertyAccessors = propertySymbol.DeclaredAccessibility;

         if (propertySymbol.IsReadOnly)
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     getAccessorDecl
                  }
               )
            );
         }
         else if (propertySymbol.IsWriteOnly)
         {
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = GetWithBaseCallAccessorDeclaration(indentation, setAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     setAccessorDecl
                  }
               )
            );
         }
         else
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = GetWithBaseCallAccessorDeclaration(string.Empty, setAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     getAccessorDecl,
                     setAccessorDecl
                  }
               )
            );
         }

         var reduceIndentation = " ".Repeat(indentation.Length / 2);
         return accessorList.WithOpenBraceToken(
               Token(
                  TriviaList(LineFeed, Whitespace(reduceIndentation)),
                  SyntaxKind.OpenBraceToken,
                  TriviaList(LineFeed)
               )
            )
            .WithCloseBraceToken(
               Token(
                  TriviaList(Whitespace(reduceIndentation)),
                  SyntaxKind.CloseBraceToken,
                  TriviaList(LineFeed)
               )
            );
      }

      private static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(string indentation,
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

      private static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(SyntaxTokenList getAccessModifiers,
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

      private static SyntaxTokenList GenerateAccessModifiers(this ISymbol methodSymbol,
         Accessibility propertyAccessibility, string indentation)
      {
         SyntaxTokenList accessModifiers;
         var leadingTrivia = Whitespace($"{indentation}");
         var trailingTrivia = Space;
         var emptyTokens = TokenList(
            Token(
               TriviaList(leadingTrivia),
               SyntaxKind.None,
               TriviaList(Whitespace(string.Empty))
            ));

         switch (methodSymbol.DeclaredAccessibility)
         {
            case NotApplicable:
               accessModifiers = emptyTokens;
               break;

            case Private
               when propertyAccessibility != Private:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;
            }

            case ProtectedAndInternal
               when propertyAccessibility != ProtectedAndInternal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  ));
            }
               break;

            case Protected
               when propertyAccessibility != Protected:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            case Internal
               when propertyAccessibility != Internal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            case ProtectedOrInternal
               when propertyAccessibility != ProtectedOrInternal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  ));
            }
               break;

            case Public
               when propertyAccessibility != Public:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PublicKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            default:
               accessModifiers = emptyTokens;
               break;
         }

         return accessModifiers;
      }

      private static SyntaxTokenList GetAccessModifiersWithOverride(this ISymbol symbol, string indentation)
      {
         var accessibility = symbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();
         var indentationTrivia = Whitespace(" ".Repeat(indentation.Length / 2));
         var leadingTrivia = LineFeed;
         var trailingTrivia = Space;

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  ));
               break;

            case ProtectedAndInternal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(leadingTrivia, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Protected:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Internal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case ProtectedOrInternal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Public:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PublicKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         tokens.Add(
            Token(
               TriviaList(),
               SyntaxKind.OverrideKeyword,
               TriviaList(trailingTrivia)
            )
         );

         var tokenCount = tokens.Count;
         var tokensWithIndentation = new SyntaxToken[tokenCount + 1];
         tokensWithIndentation[0] = Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.None,
            TriviaList(LineFeed));
         for (var i = 1; i < tokensWithIndentation.Length; i++)
         {
            tokensWithIndentation[i] = tokens[i - 1];
         }

         var accessTokens = TokenList(tokensWithIndentation);

         return accessTokens;
      }

      public static MethodDeclarationSyntax BuildOverridableMethod(IMethodSymbol methodSymbol,
         string indentation = DefaultIndentation)
      {
         var methodName = methodSymbol.Name;
         var returnType = methodSymbol.ReturnType.GetReturnTypeToDisplay();

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
                  .WithRefKeyword(GetRefToken()), methodIdentifier)
               : MethodDeclaration(returnTypeIdentifier, methodIdentifier)
            : MethodDeclaration(
               RefType(returnTypeIdentifier)
                  .WithRefKeyword(GetRefToken())
                  .WithReadOnlyKeyword(GetReadonlyToken()),
               methodIdentifier);

         var methodModifiers = methodSymbol.GetOverridableMethodModifiers(indentation);
         methodDecl = methodDecl.WithModifiers(methodModifiers);

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;

            if (typeParameters.Length > 0)
            {
               var typeParameterList = new List<SyntaxNodeOrToken>(typeParameters.Length * 2);
               for (var i = 0; i < typeParameters.Length; i++)
               {
                  var typeParameterSymbol = typeParameters[i];
                  typeParameterList.Add(TypeParameter(Identifier(typeParameterSymbol.Name)));
                  if (i != typeParameters.Length - 1)
                  {
                     typeParameterList.Add(Token(
                        TriviaList(),
                        SyntaxKind.CommaToken,
                        TriviaList(Space))
                     );
                  }
               }

               var genericParametersSyntax = TypeParameterList(
                  SeparatedList<TypeParameterSyntax>(typeParameterList)
               );

               methodDecl = methodDecl.WithTypeParameterList(genericParametersSyntax);
            }
         }

         return methodDecl;
      }

      private static SyntaxTokenList GetOverridableMethodModifiers(this IMethodSymbol methodSymbol, string indentation)
      {
         var accessibility = methodSymbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case ProtectedAndInternal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Protected:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Internal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case ProtectedOrInternal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Public:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PublicKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         tokens.Add(Token(
            TriviaList(),
            SyntaxKind.OverrideKeyword,
            TriviaList(Space)
         ));

         var tokenList = TokenList(tokens);

         return tokenList;
      }

      public static SyntaxToken GenerateOpenBrace(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.OpenBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GenerateCloseBrace(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.CloseBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GenerateReturn(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.ReturnKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GenerateSemicolon() =>
         Token(
            TriviaList(),
            SyntaxKind.SemicolonToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GetRefToken() =>
         Token(
            TriviaList(),
            SyntaxKind.RefKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetReadonlyToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ReadOnlyKeyword,
            TriviaList(Space)
         );

      public static ArgumentListSyntax GenerateArgumentList(ImmutableArray<IParameterSymbol> parameters) =>
         throw new NotImplementedException();

      public static void GenerateParameterList(ImmutableArray<IParameterSymbol> parameters)
      {
         throw new NotImplementedException();
      }

      public static InvocationExpressionSyntax GenerateBaseInvocation(string identifier,
         ImmutableArray<IParameterSymbol> parameters)
      {
         var invocationExpr = InvocationExpression(
            MemberAccessExpression(
               SyntaxKind.SimpleMemberAccessExpression,
               BaseExpression(),
               IdentifierName(identifier)
            )
         );
         invocationExpr = invocationExpr.WithArgumentList(GenerateArgumentList(parameters));
         return invocationExpr;
      }

      public static RefExpressionSyntax WrapWithRef(ExpressionSyntax expr)
      {
         var refExpr = RefExpression(expr).WithRefKeyword(GetRefToken());
         return refExpr;
      }

      public static ReturnStatementSyntax WrapWithReturn(ExpressionSyntax expr, string indentation)
      {
         var returnStmt = ReturnStatement(expr)
            .WithReturnKeyword(GenerateReturn(indentation))
            .WithSemicolonToken(GenerateSemicolon());
         return returnStmt;
      }

      public static BlockSyntax GenerateMethodBody(ReturnStatementSyntax returnStmt, string indentation)
      {
         var block = Block(SingletonList<StatementSyntax>(returnStmt))
            .WithOpenBraceToken(GenerateOpenBrace(indentation))
            .WithCloseBraceToken(GenerateCloseBrace(indentation));
         return block;
      }
   }
}