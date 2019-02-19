using System;
using System.Collections.Generic;
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

      public static PropertyDeclarationSyntax BuildProperty(IPropertySymbol propertySymbol,
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

         return accessorList.WithOpenBraceToken(
               Token(
                  TriviaList(LineFeed),
                  SyntaxKind.OpenBraceToken,
                  TriviaList(LineFeed)
               )
            )
            .WithCloseBraceToken(
               Token(
                  TriviaList(),
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
         var leadingTrivia = LineFeed;
         var trailingTrivia = Space;

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.Add(
                  Token(
                     TriviaList(leadingTrivia, Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  ));
               break;

            case ProtectedAndInternal:
               tokens.Add(
                  Token(
                     TriviaList(),
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
                     TriviaList(leadingTrivia, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Internal:
               tokens.Add(
                  Token(
                     TriviaList(leadingTrivia, Whitespace(indentation)),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case ProtectedOrInternal:
               tokens.Add(
                  Token(
                     TriviaList(leadingTrivia, Whitespace(indentation)),
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
                     TriviaList(leadingTrivia, Whitespace(indentation)),
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

         var accessTokens = TokenList(tokens);

         return accessTokens;
      }
   }
}