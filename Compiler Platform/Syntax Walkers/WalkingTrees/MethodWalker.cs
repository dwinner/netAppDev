using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WalkingTrees
{
   public sealed class MethodWalker : CSharpSyntaxWalker
   {
      public MethodWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
         : base(depth)
      {
      }

      public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
      {
         var parameters =
            node.ParameterList.Parameters.Select(
               parameter => $"{parameter.Type.ToFullString().Trim()} {parameter.Identifier.Text}").ToList();

         Console.WriteLine($"{node.Identifier.Text}({string.Join(", ", parameters)})");
         base.VisitMethodDeclaration(node);
      }
   }
}