using System;
using System.Collections.Generic;

namespace BubbleSorter
{
   internal static class BubbleSorter
   {
      /// <summary>
      /// Пузырьковая сортировка массива
      /// </summary>
      /// <typeparam name="T">Тип лежащих в основе элементов</typeparam>
      /// <param name="sortArray">Динамический массив для сортировки</param>
      /// <param name="comparison">
      ///   Возвращает true, если первый аргумент меньше второго, false - в противном случае
      /// </param>
      /// <param name="reversed">true, если нужна сортировка по возрастанию</param>
      internal static void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison, bool reversed = false)
      {
         bool swapped;
         do
         {
            swapped = false;
            for (int i = 0; i < sortArray.Count - 1; i++)
            {
               T first = reversed ? sortArray[i] : sortArray[i + 1];
               T second = reversed ? sortArray[i + 1] : sortArray[i];
               if (comparison(first, second))
               {
                  T temp = sortArray[i];
                  sortArray[i] = sortArray[i + 1];
                  sortArray[i + 1] = temp;
                  swapped = true;
               }
            }
         }
         while (swapped);
      }
   }
}