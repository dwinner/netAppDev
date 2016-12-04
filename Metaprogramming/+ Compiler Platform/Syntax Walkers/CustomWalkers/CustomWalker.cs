using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CustomWalkers
{
   public class CustomWalker : CSharpSyntaxWalker
   {
      private static int _tabs;

      public CustomWalker() : base(SyntaxWalkerDepth.Token) { }

      public override void Visit(SyntaxNode node)
      {
         _tabs++;
         var indents = new string('\t', _tabs);
         Console.WriteLine("{0}{1}", indents, node.Kind());
         base.Visit(node);
         _tabs--;
      }

      public override void VisitToken(SyntaxToken token)
      {
         var indents = new string('\t', _tabs);
         Console.WriteLine("{0}{1}", indents, token);
         base.VisitToken(token);
      }
   }
}