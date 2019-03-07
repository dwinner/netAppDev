using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class TokenGeneration
   {
      public static SyntaxToken GetGreaterThanToken() =>
         Token(
            TriviaList(),
            SyntaxKind.GreaterThanToken,
            TriviaList(Space)
         );

      public static SyntaxToken GetAsteriskToken() =>
         Token(
            TriviaList(),
            SyntaxKind.AsteriskToken,
            TriviaList(Space)
         );

      public static SyntaxToken GetCloseBracketToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CloseBracketToken,
            TriviaList(Space)
         );

      public static SyntaxToken GetCommaToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CommaToken,
            TriviaList(Space));

      public static SyntaxToken GetParamsToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ParamsKeyword,
            TriviaList(Space));

      public static SyntaxToken GetRefToken() =>
         Token(
            TriviaList(),
            SyntaxKind.RefKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetCloseParenToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CloseParenToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GetOutToken() =>
         Token(
            TriviaList(),
            SyntaxKind.OutKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetReadonlyToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ReadOnlyKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetOverrideToken() =>
         Token(
            TriviaList(),
            SyntaxKind.OverrideKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetPublicToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.PublicKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetInternalToken() =>
         Token(
            TriviaList(),
            SyntaxKind.InternalKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetInternalToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.InternalKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetProtectedToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.ProtectedKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetProtectedToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ProtectedKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetPrivateToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.PrivateKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetOpenBraceToken(string indentation) =>
         Token(
            TriviaList(LineFeed, Whitespace(indentation)),
            SyntaxKind.OpenBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GetCloseBraceToken(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.CloseBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GetNoneToken(SyntaxTrivia leadingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.None,
            TriviaList(Whitespace(String.Empty))
         );

      public static SyntaxToken GetPublicToken(SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.PublicKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetInternalToken(SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(),
            SyntaxKind.InternalKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetProtectedToken(SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.ProtectedKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetInternalToken(SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.InternalKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetProtectedTokens(SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.ProtectedKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetProtectedToken(SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(),
            SyntaxKind.ProtectedKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetPrivateToken(SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia),
            SyntaxKind.PrivateKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetProtectedToken(string indentation, SyntaxTrivia leadingTrivia, SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(leadingTrivia, Whitespace(indentation)),
            SyntaxKind.ProtectedKeyword,
            TriviaList(trailingTrivia)
         );

      public static SyntaxToken GetOverrideToken(SyntaxTrivia trailingTrivia) =>
         Token(
            TriviaList(),
            SyntaxKind.OverrideKeyword,
            TriviaList(trailingTrivia)
         );
   }

   public static class SyntaxKindExtensions
   {
      public static SyntaxToken BuildToken(this SyntaxKind @this)
      {

      }
   }
}