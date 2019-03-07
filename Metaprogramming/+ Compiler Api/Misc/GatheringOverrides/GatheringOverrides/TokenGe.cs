using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

static internal class TokenGe
{
   public static SyntaxToken GetOverrideToken(SyntaxTrivia trailingTrivia) =>
      SyntaxFactory.Token(
         SyntaxFactory.TriviaList(),
         SyntaxKind.OverrideKeyword,
         SyntaxFactory.TriviaList(trailingTrivia)
      );
}