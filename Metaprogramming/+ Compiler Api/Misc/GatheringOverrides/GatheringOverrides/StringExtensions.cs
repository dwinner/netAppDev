using System.Linq;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class StringExtensions
   {
      public static string Repeat(this string @this, int repeatCount) =>
         Enumerable.Repeat(@this, repeatCount)
            .Aggregate(string.Empty, (current, toAccumulate) => $"{current}{toAccumulate}");

      public static SyntaxToken ToIdentifier(this string identifier) =>
         Identifier(
            TriviaList(),
            identifier,
            TriviaList(Space));
   }
}