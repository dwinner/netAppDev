using System.Collections.Generic;

namespace ValidatingSymbolUsage
{
   /// <summary>
   ///    Scope
   /// </summary>
   public interface IScope
   {
      /// <summary>
      ///    Scope name
      /// </summary>
      string ScopeName { get; }

      /// <summary>
      ///    Where to look next for symbols
      /// </summary>
      IScope EnclosingScope { get; }

      /// <summary>
      ///    Where to look nested symbols
      /// </summary>
      IScope NestedScope { get; set; }

      IDictionary<string, Symbol> Symbols { get; }

      /// <summary>
      ///    Define the symbol in the current scope
      /// </summary>
      /// <param name="aSymbol">Symbol</param>
      void Define(Symbol aSymbol);

      /// <summary>
      ///    Look up symbol name in this scope or in enclosing scope if not here
      /// </summary>
      /// <param name="aSymbolName">Symbol name</param>
      /// <returns>Found symbol (or empty if not found)</returns>
      Symbol Resolve(string aSymbolName);
   }
}