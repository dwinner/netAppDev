using System;

namespace Strategy
{
   public class SecondStrategy : IStrategy
   {
      public void Algorithm()
      {
         Console.WriteLine("Выполняется алгоритм стратегии 2");
      }
   }
}