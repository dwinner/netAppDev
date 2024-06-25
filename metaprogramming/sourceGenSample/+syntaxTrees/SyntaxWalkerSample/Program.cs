using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxWalkerSample
{
   internal static class Program
   {
      private static void Main()
      {
         var tree = CSharpSyntaxTree.ParseText(@"class Test
{
	static void Main()
	{
		if (true)
		if (true);
	};
}");

         var root = tree.GetRoot();

         var ifCounter = new IfCounter();
         ifCounter.Visit(root);
         Console.WriteLine($"I found {ifCounter.IfCount} if statements");

         var ifCount = root.DescendantNodes().OfType<IfStatementSyntax>().Count();
         Console.WriteLine(ifCount);
      }
   }

   internal class IfCounter : CSharpSyntaxWalker
   {
      public int IfCount { get; private set; }

      public override void VisitIfStatement(IfStatementSyntax node)
      {
         IfCount++;
         // Call the base method if you want to descend into children.
         base.VisitIfStatement(node);
      }
   }
}