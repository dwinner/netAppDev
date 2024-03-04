using System;
using Microsoft.CodeAnalysis.CSharp;

namespace Traversing_RootNode
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
         Console.WriteLine(root.GetType().Name); // CompilationUnitSyntax
      }
   }
}