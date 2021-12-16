using System.Collections.Generic;
using CaplGrammar.Application.Poco;

namespace CaplGrammar.Application.Contract
{
   /// <summary>
   ///    Scope
   /// </summary>
   internal interface IScope
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
      ///    Symbols in current scope
      /// </summary>
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