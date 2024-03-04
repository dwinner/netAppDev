using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace TriviaSample
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Program
{
    static /*comment*/ void Main() {}
}");

         var root = tree.GetRoot();

         // Find the static keyword token:
         var method = root.DescendantTokens().Single(t =>
            t.Kind() == SyntaxKind.StaticKeyword);

         // Print out the trivia around the static keyword token:
         foreach (var t in method.LeadingTrivia)
         {
            Console.WriteLine(new { Kind = "Leading " + t.Kind(), t.Span.Length });
         }

         foreach (var t in method.TrailingTrivia)
         {
            Console.WriteLine(new { Kind = "Trailing " + t.Kind(), t.Span.Length });
         }
      }
   }
}