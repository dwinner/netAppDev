using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoArrangeClassMembers
{
   public sealed class AutoArrangeWalker : CSharpSyntaxWalker
   {
      public List<PropertyDeclarationSyntax> PropertyDeclarations { get; } = new List<PropertyDeclarationSyntax>();

      public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
      {
         PropertyDeclarations.Add(node);
         base.VisitPropertyDeclaration(node);
      }
   }
}