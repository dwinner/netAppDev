using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoPropertyRewriterSample
{
   public class RemoveBackingFieldRewriter : CSharpSyntaxRewriter
   {
      private readonly IEnumerable<string> _fieldsToRemove;

      public RemoveBackingFieldRewriter(params string[] fieldsToRemove)
      {
         _fieldsToRemove = fieldsToRemove;
      }

      public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
      {
         return _fieldsToRemove.Contains(node.GetText().ToString()) ? null : base.VisitFieldDeclaration(node);
      }
   }
}