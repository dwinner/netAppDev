namespace ValidatingSymbolUsage
{
   public sealed class GlobalSpaceScope : ScopeBase
   {
      public GlobalSpaceScope(IScope aScope)
         : base(aScope)
      {
      }

      public override string ScopeName => "Globals";  // TODO: Introduce enum for it or something unique

      public override string ToString() => ScopeName;
   }
}