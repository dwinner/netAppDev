namespace ValidatingSymbolUsage
{
   public class LocalScope : ScopeBase
   {
      public LocalScope(IScope aScope)
         : base(aScope)
      {
      }

      public override string ScopeName => "Locals"; // TODO: Introduce something unique for that
   }
}