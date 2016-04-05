/**
 * Мемоизация
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _12_Memoize
{
   static class Program
   {
      static void Main()
      {
         var stimer = new Stopwatch();
         stimer.Start();
         SlowFib();
         stimer.Stop();
         Console.WriteLine("Прямое вычисление Фибоначчи: {0} миллисекунд",
            stimer.ElapsedMilliseconds);

         stimer.Reset();
         stimer.Start();
         FastFib();
         stimer.Stop();
         Console.WriteLine("Вычисление Фибоначчи с мемоизацией: {0} миллисекунд",
            stimer.ElapsedMilliseconds);

         stimer.Reset();
         stimer.Start();
         ReciprocalFibonacciConstant_Slow();
         stimer.Stop();
         Console.WriteLine("Вычисление обратной константы Фибоначчи: {0} миллисекунд",
            stimer.ElapsedMilliseconds);

         stimer.Reset();
         stimer.Start();
         ReciprocalFibonacciConstant_Fast();
         stimer.Stop();
         Console.WriteLine("Вычисление обратной константы Фибоначчи с мемоизацией: {0} миллисекунд",
            stimer.ElapsedMilliseconds);

         Console.ReadKey();
      }

      static void SlowFib()   // Версия с прямым вычислением
      {
         Func<int, int> fib = null;
         fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
         for (int i = 30; i < 40; i++)
         {
            Console.WriteLine(fib(i));
         }
      }

      static void FastFib()   // Версия с мемоизацией результатов и меньшей нагрузкой на стек
      {
         Func<int, int> fib = null;
         fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
         fib = fib.Memoize();
         for (int i = 30; i < 40; i++)
         {
            Console.WriteLine(fib(i));
         }
      }

      static void ReciprocalFibonacciConstant_Slow()
      {
         Func<ulong, ulong> fib = null;
         fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
         Func<ulong, decimal> fibConstant = null;
         fibConstant = x => x == 1 ? 1 / ((decimal)fib(x)) : 1 / ((decimal)fib(x)) + fibConstant(x - 1);
         Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\n",
            "Номер",
            "Фибоначчи".PadRight(24),
            "1/Фибоначчи ".PadRight(24),
            "Константа Фибоначчи".PadRight(24));
         for (ulong i = 1; i < 93; i++)
         {
            Console.WriteLine("{0:D5}\t{1:D24}\t{2:F24}\t{3:F24}",
               i,
               fib(i),
               (1 / (decimal)fib(i)),
               fibConstant(i));
         }
      }

      static void ReciprocalFibonacciConstant_Fast()
      {
         Func<ulong, ulong> fib = null;
         fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
         fib = fib.Memoize();
         Func<ulong, decimal> fibConstant = null;
         fibConstant = x => x == 1 ? 1 / ((decimal)fib(x)) : 1 / ((decimal)fib(x)) + fibConstant(x - 1);
         fibConstant = fibConstant.Memoize();
         Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\n",
            "Номер",
            "Фибоначчи".PadRight(24),
            "1/Фибоначчи ".PadRight(24),
            "Константа Фибоначчи".PadRight(24));
         for (ulong i = 1; i < 93; i++)
         {
            Console.WriteLine("{0:D5}\t{1:D24}\t{2:F24}\t{3:F24}",
               i,
               fib(i),
               (1 / (decimal)fib(i)),
               fibConstant(i));
         }
      }
   }

   public static class Memoizers
   {
      public static Func<T, TR> Memoize<T, TR>(this Func<T, TR> func)   // Запоминание результатов вызовов делегата
      {
         IDictionary<T, TR> cache = new Dictionary<T, TR>();
         return x =>
            {
               TR result;
               if (cache.TryGetValue(x, out result))
                  return result;
               result = func(x);
               cache[x] = result;
               return result;
            };
      }
   }
}
