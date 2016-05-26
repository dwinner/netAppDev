using System;
using Microsoft.CodeAnalysis;

namespace SymbolVisitorSample
{
   public class NamedTypeVisitor : SymbolVisitor
   {
      public override void VisitNamespace(INamespaceSymbol symbol)
      {
         Console.WriteLine(symbol);

         foreach (var childSymbol in symbol.GetMembers())
         {
            childSymbol.Accept(this);
         }
      }

      public override void VisitNamedType(INamedTypeSymbol symbol)
      {
         Console.WriteLine(symbol);

         foreach (var child in symbol.GetTypeMembers())
         {
            child.Accept(this);
         }
      }
   }
}