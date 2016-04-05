using System;

namespace Strategy
{
   public class FirstStrategy : IStrategy
   {
      public void Algorithm()
      {
         Console.WriteLine("Выполняется алгоритм стратегии 1");
      }
   }
}