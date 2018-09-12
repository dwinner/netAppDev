#region Using directives

using System;
using System.ComponentModel;
using System.Linq;

#endregion

namespace Multithreading
{
   public static class Worker
   {
      public static int[] FindPrimes(int fromNumber, int toNumber, BackgroundWorker backgroundWorker)
      {
         var list = new int[toNumber - fromNumber];

         // Create an array containing all integers between the two specified numbers.
         for (var i = 0; i < list.Length; i++)
         {
            list[i] = fromNumber;
            fromNumber += 1;
         }

         //find out the module for each item in list, divided by each d, where
         //d is < or == to sqrt(to)
         //if the remainder is 0, the nubmer is a composite, and thus
         //we mark its position with 0 in the marks array,
         //otherwise the number is a prime, and thus mark it with 1
         var maxDiv = (int) Math.Floor(Math.Sqrt(toNumber));
         var mark = new int[list.Length];

         for (var i = 0; i < list.Length; i++)
         {
            for (var j = 2; j <= maxDiv; j++)
               if (list[i] != j && list[i] % j == 0)
                  mark[i] = 1;

            var iteration = list.Length / 100;
            if (i % iteration == 0 && backgroundWorker != null)
            {
               if (backgroundWorker.CancellationPending)
                  return null;

               if (backgroundWorker.WorkerReportsProgress)
                  backgroundWorker.ReportProgress(i / iteration);
            }
         }

         //create new array that contains only the primes, and return that array
         var primes = mark.Count(t => t == 0);

         var ret = new int[primes];
         var curs = 0;
         for (var i = 0; i < mark.Length; i++)
            if (mark[i] == 0)
            {
               ret[curs] = list[i];
               curs += 1;
            }

         if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
            backgroundWorker.ReportProgress(100);

         return ret;
      }
   }
}