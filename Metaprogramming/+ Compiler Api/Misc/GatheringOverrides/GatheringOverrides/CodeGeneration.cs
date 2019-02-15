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
         );

         var propertyModifiers = propertySymbol.GetAccessModifiersWithOverride(indentation);
         propertyDecl = propertyDecl.WithModifiers(propertyModifiers);
         var accessorList = propertySymbol.GetAccessorList(indentation);
         propertyDecl = propertyDecl.WithAccessorList(accessorList);

         return propertyDecl;
      }

      private static AccessorListSyntax GetAccessorList(this IPropertySymbol propertySymbol, string indentation)
      {
         var propertyName = propertySymbol.Name;
         AccessorListSyntax accessorList;

         if (propertySymbol.IsReadOnly)
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(indentation);

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
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(indentation);

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
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(indentation);

            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);

            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(indentation);

            var setAccessorDecl = GetWithBaseCallAccessorDeclaration(indentation, setAccessModifiers, propertyName);

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
                  TriviaList(Whitespace(indentation)),
                  SyntaxKind.OpenBraceToken,
                  TriviaList(LineFeed)
               )
            )
            .WithCloseBraceToken(
               Token(
                  TriviaList(Whitespace(indentation)),
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
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
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

      private static SyntaxTokenList GenerateAccessModifiers(this ISymbol methodSymbol, string indentation)
      {
         SyntaxTokenList accessModifiers;

         switch (methodSymbol.DeclaredAccessibility)
         {
            case NotApplicable:
               accessModifiers = new SyntaxTokenList(Array.Empty<SyntaxToken>());
               break;

            case Private:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case ProtectedAndInternal:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  ));
               break;

            case Protected:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case Internal:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case ProtectedOrInternal:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  ));
               break;

            case Public:
               accessModifiers = TokenList(
                  Token(
                     TriviaList(Whitespace(string.Format("{0}{0}", indentation))),
                     SyntaxKind.PublicKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         return accessModifiers;
      }

      private static SyntaxTokenList GetAccessModifiersWithOverride(this ISymbol symbol, string indentation)
      {
         var accessibility = symbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();
         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  ));
               break;

            case ProtectedAndInternal:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case Protected:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case Internal:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case ProtectedOrInternal:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               );
               break;

            case Public:
               tokens.Add(
                  Token(
                     TriviaList(LineFeed, Whitespace(indentation)),
                     SyntaxKind.PublicKeyword,
                     TriviaList(Space)
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
               TriviaList(Space)
            )
         );

         var accessTokens = TokenList(tokens);

         return accessTokens;
      }
   }
}