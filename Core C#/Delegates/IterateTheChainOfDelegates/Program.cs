/** 
 * Итерация по цепочкам делегатов.
 */

using System;

namespace _03_IterateTheChainOfDelegates
{
   class Program
   {
      static void Main(string[] args)
      {
         Processor proc1 = new Processor(0.75);
         Processor proc2 = new Processor(0.83);
         ProcessResults[] delegates = new ProcessResults[]
         {
            proc1.Compute,
            proc2.Compute,
            Processor.StaticCompute
         };

         ProcessResults chained = delegates[0] + delegates[1] + delegates[2];

         Delegate[] chain = chained.GetInvocationList();
         double accumulator = 0;
         for (int i = 0; i < chain.Length; i++)
         {
            ProcessResults current = chain[i] as ProcessResults;
            if (current != null)
               accumulator += current(4, 5);
         }

         Console.WriteLine("Output: {0}", accumulator);
         Console.ReadKey();
      }
   }

   public delegate double ProcessResults(double x, double y);

   public class Processor
   {
      private double _factor;

      public Processor(double factor)
      {
         _factor = factor;
      }

      public double Compute(double x, double y)
      {
         double result = (x + y) * _factor;
         Console.WriteLine("Instance results: {0}", result);
         return result;
      }

      public static double StaticCompute(double x, double y)
      {
         double result = (x + y) * 0.5;
         Console.WriteLine("Static compute: {0}", result);
         return result;
      }
   }
}
