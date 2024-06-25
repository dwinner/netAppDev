using System;
using Microsoft.CodeAnalysis.CSharp;

namespace ObtainingSyntaxTree
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Test
{
	static void Main() => Console.WriteLine (""Hello"");
}");

         Console.WriteLine(tree.ToString());
      }
   }
}