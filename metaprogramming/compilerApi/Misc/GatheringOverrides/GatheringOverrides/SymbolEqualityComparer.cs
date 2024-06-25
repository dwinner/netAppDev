using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace GatheringOverrides
{
   public sealed class SymbolEqualityComparer : IEqualityComparer<ISymbol>
   {
      public static IEqualityComparer<ISymbol> Default { get; } = new SymbolEqualityComparer();

      public bool Equals(ISymbol x, ISymbol y) => x.Name.Equals(y.Name, StringComparison.CurrentCulture);

      public int GetHashCode(ISymbol obj) => obj.Name.GetHashCode();
   }
}