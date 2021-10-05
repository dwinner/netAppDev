using System.Collections.Generic;
using System.Linq;

namespace ValidatingSymbolUsage
{
   public class FunctionSymbol : Symbol, IScope
   {
      private readonly Dictionary<string, Symbol> _arguments = new();

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

      public void Define(Symbol aSymbol)
      {
         if (_arguments.ContainsKey(aSymbol.Name))
         {
            return;
         }

         _arguments[aSymbol.Name] = aSymbol;
         aSymbol.Scope = this;
      }

      public Symbol Resolve(string aSymbolName) =>
         _arguments.TryGetValue(aSymbolName, out var symbolName)
            ? symbolName
            : EnclosingScope?.Resolve(aSymbolName) ?? Null;

      public override string ToString() =>
         $"function {base.ToString()}:{_arguments.Values.Aggregate(string.Empty, (current, symbol) => $"{current}{symbol}, ")}";
   }
}