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
      public const string DefaultIndentation = "    ";

      public static PropertyDeclarationSyntax BuildProperty(IPropertySymbol propertySymbol,
         string indentation = DefaultIndentation)
      {
         var propertyName = propertySymbol.Name;
         var propertyType = propertySymbol.Type.NormalizeTypeSymbol();
         var propertyDecl = PropertyDeclaration(
            IdentifierName(
               Identifier(
                  TriviaList(), propertyType, TriviaList(Space)
               )
            ),
            Identifier(
               TriviaList(), propertyName, TriviaList(Space)
            )
         );

         var propertyModifiers = propertySymbol.GetAccessModifiers(indentation);
         propertyDecl = propertyDecl.WithModifiers(propertyModifiers);

         return propertyDecl;
      }

      public static SyntaxTokenList GetAccessModifiers(this ISymbol symbol, string indentation)
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