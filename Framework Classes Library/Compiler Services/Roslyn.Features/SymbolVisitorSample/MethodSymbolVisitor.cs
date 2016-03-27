using System;
using Microsoft.CodeAnalysis;

namespace SymbolVisitorSample
{
   public class MethodSymbolVisitor : SymbolVisitor
   {
      public override void VisitNamespace(INamespaceSymbol symbol)
      {
         foreach (var child in symbol.GetMembers())
         {
            child.Accept(this);
         }
      }

      public override void VisitNamedType(INamedTypeSymbol symbol)
      {
         foreach (var child in symbol.GetMembers())
         {
            child.Accept(this);
         }
      }

      public override void VisitMethod(IMethodSymbol symbol)
      {
         Console.WriteLine(symbol);
      }
   }
}