using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class SyntaxKindExtensions
   {
      public static SyntaxToken BuildToken(this SyntaxKind @this,
         SyntaxTrivia[] leading, SyntaxTrivia[] trailing) =>
         Token(
            leading == null || leading.Length == 0
               ? TriviaList()
               : TriviaList(leading),
            @this,
            trailing == null || trailing.Length == 0
               ? TriviaList()
               : TriviaList(trailing));
   }
}