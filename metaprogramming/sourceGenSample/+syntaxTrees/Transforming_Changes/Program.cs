using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Transforming_Changes
{
   internal static class Program
   {
      private static void Main()
      {
         var sourceText = SourceText.From("class Program {}");
         var tree = CSharpSyntaxTree.ParseText(sourceText);
         var newSource = sourceText.Replace(0, 5, "struct");
         var newTree = tree.WithChangedText(newSource);
         Console.WriteLine(newTree.ToString()); // struct Program {}
      }
   }
}