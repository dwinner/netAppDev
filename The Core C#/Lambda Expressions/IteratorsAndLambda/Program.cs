/**
 * Итераторы и лямбда-выражения
 */

using System;
using System.Collections.Generic;

namespace _09_IteratorsAndLambda
{
   class Program
   {
      static void Main()
      {
         var matrix = new List<List<double>>
         {
            new List<double> { 1.0, 1.1, 1.2 },
            new List<double> { 2.0, 2.1, 2.2 },
            new List<double> { 3.0, 3.1, 3.2 }
         };

         // Итератор для прохода по диагонали матрицы
         var iter = matrix.MakeCustomIterator(
            new[] { 0, 0 },
            (coll, cur) => coll[cur[0]][cur[1]],
            cur => cur[0] > 2 || cur[1] > 2,
            cur => new[] { cur[0] + 1, cur[1] + 1 });

         foreach (var item in iter)
         {
            Console.WriteLine(item);
         }

         Console.ReadKey();
      }
   }

   public static class IteratorExtensions
   {
      /// <summary>
      /// Расширение для обобщенной итерации по типу
      /// </summary>
      /// <typeparam name="TCollection">Параметр типа для итерации (не обязательно коллекция)</typeparam>
      /// <typeparam name="TCursor">Параметр типа курсора</typeparam>
      /// <typeparam name="TItem">Параметр типа элемента итератора</typeparam>
      /// <param name="collection">Тип для итерации</param>
      /// <param name="cursor">Курсор для типа итерации</param>
      /// <param name="getCurrent">Делегат текущего элемента</param>
      /// <param name="isFinished">Делегат признака окончания итерации</param>
      /// <param name="advanceCursor">Делегат продвижения курсора к следующему элементу</param>
      /// <returns>Обобщенный итератор типа</returns>
      public static IEnumerable<TItem> MakeCustomIterator<TCollection, TCursor, TItem>(
         this TCollection collection,
         TCursor cursor,
         Func<TCollection, TCursor, TItem> getCurrent,
         Func<TCursor, bool> isFinished,
         Func<TCursor, TCursor> advanceCursor)
      {
         while (!isFinished(cursor))
         {
            yield return getCurrent(collection, cursor);
            cursor = advanceCursor(cursor);
         }
      }
   }
}
