/**
 * Цепочки делегатов
 */

using System;

namespace _02_ChainOfDelegates
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

         // Связать делегаты в цепочку.
         ProcessResults chained = null;   //ProcessResults chained = delegates[0] + delegates[1] + delegates[2];
         foreach (var item in delegates)
         {
            chained += item;
         }
         double combined = chained(4, 5);

         Console.WriteLine("Output: {0}", combined);
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
