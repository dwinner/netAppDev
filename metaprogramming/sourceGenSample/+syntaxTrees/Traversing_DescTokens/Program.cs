using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace Traversing_DescTokens
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Test
{
	static void Main() => Console.WriteLine (""Hello"");
}");

         var root = tree.GetRoot();
         foreach (var x1 in root.DescendantTokens().Select(t => new { Kind = t.Kind(), t.Text }))
         {
            Console.WriteLine(x1);
         }
      }
   }
}