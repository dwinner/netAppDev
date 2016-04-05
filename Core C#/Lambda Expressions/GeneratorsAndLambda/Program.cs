/**
 * Генераторы чисел и лямбда-выражения
 */

using System;
using System.Collections.Generic;

namespace _10_GeneratorsAndLambda
{
   class Program
   {
      static void Main()
      {
         var iter = MakeGenerator<double>(1, x => x * 1.2);
         var enumerator = iter.GetEnumerator();
         for (int i = 0; i < 10; i++)
         {
            enumerator.MoveNext();
            Console.WriteLine(enumerator.Current);
         }

         Console.ReadLine();
      }

      /// <summary>
      /// Итератор, создающий определенную последовательность
      /// </summary>
      /// <typeparam name="T">Параметр типа последовательности</typeparam>
      /// <param name="initialValue">Начальное значение</param>
      /// <param name="advance">Делегат, возвращающий следующий элемент последовательности</param>
      /// <returns>Итератор, создающий определенную последовательность</returns>
      static IEnumerable<T> MakeGenerator<T>(T initialValue, Func<T, T> advance)
      {
         T currentValue = initialValue;
         while (true)
         {
            yield return currentValue;
            currentValue = advance(currentValue);
         }
      }
   }
}
