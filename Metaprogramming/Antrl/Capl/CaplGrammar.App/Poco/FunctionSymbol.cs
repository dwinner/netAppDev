using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaplGrammar.Application.Contract;

namespace CaplGrammar.Application.Poco
{
   internal class FunctionSymbol : Symbol, IScope
   {
      public FunctionSymbol(string aSymbolName)
         : base(aSymbolName)
      {
      }

      public FunctionSymbol(string aSymbolName, BuiltInType aType)
         : base(aSymbolName, aType)
      {
      }

      public FunctionSymbol(string aSymbolName, BuiltInType aReturnType, IScope anEnclosingScope, string userDefined)
         : this(aSymbolName, aReturnType)
      {
         EnclosingScope = anEnclosingScope;
         UserDefinedType = userDefined;
      }

      //public IScope NestedScope { get; set; }

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

      public override string ToString()
      {
         var debugInfo = new StringBuilder()
            .AppendLine($"Function: {base.ToString()}")
            .AppendLine(Symbols.Values.Aggregate(string.Empty,
               (current, symbol) => $"{current}{symbol}{Environment.NewLine}"));

         return debugInfo.ToString();
      }
   }
}