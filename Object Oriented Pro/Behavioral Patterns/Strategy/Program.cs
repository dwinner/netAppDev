/**
 * Стратегии
 */

namespace Strategy
{
   public static class Program
   {
      public static void Main()
      {
         var context = new StrategyContext(new FirstStrategy());
         context.Execute();
         context.Strategy = new SecondStrategy();
         context.Execute();
      }
   }
}
