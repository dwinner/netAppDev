using System;
using System.Collections.Generic;
using System.Linq;
using CaplGrammar.Application.Poco;

namespace CaplGrammar.Application.Contract
{
   internal abstract class ScopeBase : IScope, IEquatable<ScopeBase>
   {
      protected ScopeBase(IScope aScope) => EnclosingScope = aScope;

      //public static IEqualityComparer<ScopeBase> DefaultComparer { get; } = new ScopeEqualityComparer();

      public bool Equals(ScopeBase other) =>
         !ReferenceEquals(null, other)
         && (ReferenceEquals(this, other) ||
             ScopeName == other.ScopeName && Equals(EnclosingScope, other.EnclosingScope));

      //public IScope NestedScope { get; set; }
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

      public override bool Equals(object obj) => !ReferenceEquals(null, obj) &&
                                                 (ReferenceEquals(this, obj) || obj.GetType() == GetType() &&
                                                    Equals((ScopeBase)obj));

      public override int GetHashCode()
      {
         unchecked
         {
            return ((ScopeName != null ? ScopeName.GetHashCode() : 0) * 397) ^
                   (EnclosingScope != null ? EnclosingScope.GetHashCode() : 0);
         }
      }

      public static bool operator ==(ScopeBase left, ScopeBase right) => Equals(left, right);

      public static bool operator !=(ScopeBase left, ScopeBase right) => !Equals(left, right);

      /*private sealed class ScopeEqualityComparer : IEqualityComparer<ScopeBase>
      {
         public bool Equals(ScopeBase x, ScopeBase y) => x.Equals(y);

         public int GetHashCode(ScopeBase obj) => obj.GetHashCode();
      }*/
   }
}