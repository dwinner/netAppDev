using System.Collections.Generic;
using System.Linq;

namespace ValidatingSymbolUsage
{
   public class FunctionSymbol : Symbol, IScope
   {
      public FunctionSymbol(string aSymbolName)
         : base(aSymbolName)
      {
      }

      public FunctionSymbol(string aSymbolName, BuiltInType aType)
         : base(aSymbolName, aType)
      {
      }

      public FunctionSymbol(string aSymbolName, BuiltInType aReturnType, IScope anEnclosingScope)
         : this(aSymbolName, aReturnType) =>
         EnclosingScope = anEnclosingScope;

      public string ScopeName => Name;

      public IScope EnclosingScope { get; }

      public IDictionary<string, Symbol> Symbols { get; } = new Dictionary<string, Symbol>();

      public void Define(Symbol aSymbol)
      {
         if (Symbols.ContainsKey(aSymbol.Name))
         {
            return;
         }

         Symbols[aSymbol.Name] = aSymbol;
         aSymbol.Scope = this;
      }

      public Symbol Resolve(string aSymbolName) =>
         Symbols.TryGetValue(aSymbolName, out var symbolName)
            ? symbolName
            : EnclosingScope?.Resolve(aSymbolName) ?? Null;

      public override string ToString() =>
         $"function {base.ToString()}:{Symbols.Values.Aggregate(string.Empty, (current, symbol) => $"{current}{symbol}, ")}";
   }
}