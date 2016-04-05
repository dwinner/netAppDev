// Конроль доступа к объекту из создавшего потока

using System;
using System.Threading;
using System.Threading.Tasks;
using PostSharp.Patterns.Threading;

namespace ThreadAffineModelSample
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            var orderService = new OrderService();
            orderService.Process(1);

            Task.Factory.StartNew(() => { orderService.Process(2); }).Wait();
         }
         catch (AggregateException aggregateEx)
         {
            Console.WriteLine(aggregateEx.GetBaseException().Message);
         }

         Console.ReadKey();
      }
   }

   [ThreadAffine]
   public class OrderService
   {
      public void Process(int sequence)
      {
         Console.WriteLine("sequence {0}", sequence);
         Console.WriteLine("sleeping for 10s");
         Thread.Sleep(TimeSpan.FromSeconds(10));
      }
   }
}