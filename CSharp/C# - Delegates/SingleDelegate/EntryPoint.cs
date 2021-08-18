/**
 * Одиночный делегат
 */

using System;

namespace _01_SingleDelegate
{
   class EntryPoint
   {
      static void Main()
      {
         var firstProcessor = new Processor(0.75);
         var secondProcessor = new Processor(0.83);

         var firstDelegate = new ProcessResults(firstProcessor.Compute);
         ProcessResults secondDelegate = new ProcessResults(secondProcessor.Compute);
         ProcessResults thirdDelegate = Processor.StaticCompute;

         double combined = firstDelegate(4, 5) + secondDelegate(6, 2) + thirdDelegate(5, 2);
         Console.WriteLine("Output : {0}", combined);
         Console.ReadKey();
      }
   }

   public delegate double ProcessResults(double x, double y);

   public class Processor
   {
      private readonly double _factor;

      public Processor(double factor)
      {
         _factor = factor;
      }

      public double Compute(double x, double y)
      {
         double result = (x + y) * _factor;
         Console.WriteLine("Instance result: {0}", result);
         return result;
      }

      public static double StaticCompute(double x, double y)
      {
         double result = (x + y) * 0.5;
         Console.WriteLine("Static result: {0}", result);
         return result;
      }
   }
}
