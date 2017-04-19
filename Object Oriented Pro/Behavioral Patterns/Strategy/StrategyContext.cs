namespace Strategy
{
   public class StrategyContext
   {
      public IStrategy Strategy { get; set; }

      public StrategyContext(IStrategy strategy)
      {
         Strategy = strategy;
      }

      public void Execute() => Strategy.Algorithm();
   }
}