using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoArrangeRefactoring
{
   public sealed class AutoArrangeReplaceRewriter : CSharpSyntaxRewriter
   {
      private int _count;
      private readonly List<SyntaxNode> _nodes=new List<SyntaxNode>();      
      private static readonly Comparison<string> _CompFunc = (x, y) => string.Compare(x, y, StringComparison.CurrentCulture);

      public AutoArrangeReplaceRewriter(AutoArrangeCaptureWalker captureWalker)
      {
         captureWalker.Constructors.Sort((firstCtor, secondCtor) => _CompFunc(firstCtor.Identifier.ValueText, secondCtor.Identifier.ValueText));
         captureWalker.Enums.Sort((firstEnum, secondEnum) => _CompFunc(firstEnum.Identifier.ValueText, secondEnum.Identifier.ValueText));
         captureWalker.Events.Sort((firstEvent, secondEvent) => _CompFunc(firstEvent.Identifier.ValueText, secondEvent.Identifier.ValueText));
         captureWalker.Fields.Sort((field1, field2) => _CompFunc(field1.Declaration.Variables[0].Identifier.ValueText, field2.Declaration.Variables[0].Identifier.ValueText));           
         captureWalker.Methods.Sort((method1, method2) => _CompFunc(method1.Identifier.ValueText, method2.Identifier.ValueText));
         captureWalker.Properties.Sort((property1, property2) => _CompFunc(property1.Identifier.ValueText, property2.Identifier.ValueText));
         captureWalker.Types.Sort((arrange1, arrange2) => _CompFunc(arrange1.Target.Identifier.ValueText, arrange2.Target.Identifier.ValueText));

         _nodes.AddRange(captureWalker.Events);
         _nodes.AddRange(captureWalker.Fields);
         _nodes.AddRange(captureWalker.Constructors);
         _nodes.AddRange(captureWalker.Methods);
         _nodes.AddRange(captureWalker.Properties);
         _nodes.AddRange(captureWalker.Enums);
         _nodes.AddRange(captureWalker.Types.Select(walker => walker.Target));         
      }

      public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitEnumDeclaration(EnumDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitEventDeclaration(EventDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node) => Replace(node);
      public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node) => base.VisitStructDeclaration(node);

      private SyntaxNode Replace(SyntaxNode node)
      {
         SyntaxNode result = null;

         if (_count<_nodes.Count)
         {
            result = _nodes[_count++];
         }
         else
         {
            throw new NotSupportedException();
         }

         return result;
      }
   }
}