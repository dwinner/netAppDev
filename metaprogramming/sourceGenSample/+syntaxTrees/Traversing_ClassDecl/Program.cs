using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Traversing_ClassDecl
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
         var cds = (ClassDeclarationSyntax)root.ChildNodes().Single();
         foreach (var member in cds.Members)
         {
            Console.WriteLine(member.ToString());
         }
      }
   }
}