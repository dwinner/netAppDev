using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace SymbolVisitorSample
{
   public class CustomSymbolFinder
   {
      public List<INamedTypeSymbol> GetAllSymbols(Compilation aCompilation)
      {
         var visitor = new FindAllSymbolsVisitor();
         visitor.Visit(aCompilation.GlobalNamespace);
         return visitor.AllTypedSymbols;
      }

      private sealed class FindAllSymbolsVisitor : SymbolVisitor
      {
         public List<INamedTypeSymbol> AllTypedSymbols { get; } = new List<INamedTypeSymbol>();

         public override void VisitNamespace(INamespaceSymbol symbol)
         {
            Parallel.ForEach(symbol.GetMembers(), typeSymbol => typeSymbol.Accept(this));
         }

         public override void VisitNamedType(INamedTypeSymbol symbol)
         {
            AllTypedSymbols.Add(symbol);
            foreach (var childSymbol in symbol.GetTypeMembers())
            {
               Visit(childSymbol);
            }
         }
      }
   }
}