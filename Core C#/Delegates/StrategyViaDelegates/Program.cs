/**
 * GOFF DP Стратегия на основе делегатов
 */

using System;
using System.Collections;

namespace _11_StrategyViaDelegates
{
   class Program
   {
      static void Main(string[] args)
      {
      }
   }

   public delegate Array SortStrategy(ICollection theCollection);

   public class Consumer
   {
      private SortStrategy strategy;
      private ArrayList myCollection;

      public SortStrategy MyProperty
      {
         get { return strategy; }
         set { strategy = value; }
      }

      public Consumer(SortStrategy defaultStrategy)
      {
         this.strategy = defaultStrategy;
      }

      public void DoSomeWork()
      {
         // Использовать стратегию
         Array sorted = strategy(myCollection);
         // Сделать что-то с результатом
      }
   }

   public class SortAgrorithms
   {
      static Array SortFast(ICollection theCollection)
      {
         throw new NotImplementedException();   // Выполнить быструю сортировку
      }

      static Array SortSlow(ICollection theCollection)
      {
         throw new NotImplementedException();   // Выполнить медленную сортировку
      }
   }
}
