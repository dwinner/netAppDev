using CaplGrammar.Application.Contract;

namespace CaplGrammar.Application.Impl.SymbolUsageValidation
{
   internal sealed class GlobalSpaceScope : ScopeBase
   {
      public GlobalSpaceScope(IScope aScope)
         : base(aScope)
      {
      }

      public override string ScopeName => "Globals";

      public override string ToString() => ScopeName;
   }
}