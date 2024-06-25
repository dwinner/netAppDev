using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace WhiteWalker
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

         var whiteWalker = new WhiteWalker();
         whiteWalker.Visit(root);
         var spaceCount = whiteWalker.SpaceCount;
         Console.WriteLine($"Spaces = {spaceCount}");
      }
   }

   internal class WhiteWalker : CSharpSyntaxWalker // Counts space characters
   {
      public WhiteWalker() : base(SyntaxWalkerDepth.Trivia)
      {
      }

      public int SpaceCount { get; private set; }

      public override void VisitTrivia(SyntaxTrivia trivia)
      {
         SpaceCount += trivia.ToString().Count(char.IsWhiteSpace);
         base.VisitTrivia(trivia);
      }
   }
}