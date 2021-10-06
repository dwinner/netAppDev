using System.Collections.Generic;
using System.Linq;

namespace ValidatingSymbolUsage
{
   public abstract class ScopeBase : IScope
   {
      private readonly Dictionary<string, Symbol> _symbols = new();

      protected ScopeBase(IScope aScope) => EnclosingScope = aScope;

      public abstract string ScopeName { get; }

      /// <summary>
      ///    Enclosing scope
      ///    <remarks>null if global (outermost) scope</remarks>
      /// </summary>
      public IScope EnclosingScope { get; }

      public void Define(Symbol aSymbol)
      {
         if (_symbols.ContainsKey(aSymbol.Name))
         {
            return;
         }

         _symbols[aSymbol.Name] = aSymbol;
         aSymbol.Scope = this; // track the scope in each symbol
      }

      public Symbol Resolve(string aSymbolName) =>
         _symbols.TryGetValue(aSymbolName, out var symbol)
            ? symbol
            : EnclosingScope?.Resolve(aSymbolName) ?? Symbol.Null;

      public override string ToString() =>
         $"{ScopeName}:{_symbols.Keys.Aggregate(string.Empty, (current, key) => $"{current}{key}, ")}";
   }
}