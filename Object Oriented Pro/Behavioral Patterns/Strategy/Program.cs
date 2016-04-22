/**
 * Стратегии
 */

namespace Strategy
{
   public static class Program
   {
      public static void Main()
      {
         // Создаём контекст и инициализируем его первой стратегией.
         var context = new StrategyContext(new FirstStrategy());

         // Выполняем операцию контекста, которая использует первую стратегию.
         context.Execute();

         // Заменяем в контексте первую стратегию второй.
         context.Strategy = new SecondStrategy();

         // Выполняем операцию контекста, которая теперь использует вторую стратегию.
         context.Execute();
      }
   }
}
