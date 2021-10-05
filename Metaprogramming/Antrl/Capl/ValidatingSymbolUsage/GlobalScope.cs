namespace ValidatingSymbolUsage
{
   public class GlobalScope : ScopeBase
   {
      public GlobalScope(IScope aScope)
         : base(aScope)
      {
      }

      public override string ScopeName => "Globals";  // TODO: Introduce enum for it or something unique

      public override string ToString() => ScopeName;
   }
}