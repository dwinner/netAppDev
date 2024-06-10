namespace Strategy
{
   public class StrategyContext
   {
      public StrategyContext(IStrategy strategy) => Strategy = strategy;

      public IStrategy Strategy { get; set; }

      public void Execute() => Strategy.Algorithm();
   }
}