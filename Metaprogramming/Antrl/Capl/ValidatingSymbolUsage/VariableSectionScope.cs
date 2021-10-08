namespace ValidatingSymbolUsage
{
   public sealed class VariableSectionScope : ScopeBase
   {
      public VariableSectionScope(IScope aScope)
         : base(aScope)
      {
      }

      // TODO: Introduce enum for it or something unique
      public override string ScopeName => "Variables";
   }
}