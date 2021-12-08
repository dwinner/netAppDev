using System.Collections.Generic;
using System.Linq;

namespace ValidatingSymbolUsage
{
   public abstract class ScopeBase : IScope
   {
      protected ScopeBase(IScope aScope) => EnclosingScope = aScope;

      public IScope NestedScope { get; set; }
      public IDictionary<string, Symbol> Symbols { get; } = new Dictionary<string, Symbol>();

      public abstract string ScopeName { get; }

      /// <summary>
      ///    Enclosing scope
      ///    <remarks>null if global (outermost) scope</remarks>
      /// </summary>
      public IScope EnclosingScope { get; }

      public void Define(Symbol aSymbol)
      {
         if (Symbols.ContainsKey(aSymbol.Name))
         {
            return;
         }

         Symbols[aSymbol.Name] = aSymbol;
         aSymbol.Scope = this; // track the scope in each symbol
      }

      public Symbol Resolve(string aSymbolName) =>
         Symbols.TryGetValue(aSymbolName, out var symbol)
            ? symbol
            : EnclosingScope?.Resolve(aSymbolName) ?? Symbol.Null;

      public override string ToString() =>
         $"{ScopeName}:{Symbols.Keys.Aggregate(string.Empty, (current, key) => $"{current}{key}, ")}";
   }
}