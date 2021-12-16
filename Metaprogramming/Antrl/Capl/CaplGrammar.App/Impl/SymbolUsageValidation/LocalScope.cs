using CaplGrammar.Application.Contract;

namespace CaplGrammar.Application.Impl.SymbolUsageValidation
{
   internal class LocalScope : ScopeBase
   {
      public LocalScope(IScope aScope)
         : base(aScope)
      {
      }

      public override string ScopeName => "Locals";
   }
}