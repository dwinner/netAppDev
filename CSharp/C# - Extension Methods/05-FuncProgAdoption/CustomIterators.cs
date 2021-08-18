using System;
using System.Collections.Generic;

namespace _05_FuncProgAdoption
{
   /// <summary>
   /// Расширяющие методы для итерации по рекурсивному списку
   /// </summary>
   public static class CustomIterators
   {
      /// <summary>
      /// Итератор прямого направления
      /// </summary>
      /// <typeparam name="T">Целевой тип объекта для списка</typeparam>
      /// <param name="aList">Объект рекурсивного списка</param>
      /// <returns>yield-Итератор</returns>
      public static IEnumerable<T> LinkListIterator<T>(this IRecursiveList<T> aList)
      {
         for (var list = aList; list.Tail != null; list = list.Tail)
         {
            yield return list.Head;
         }
      }

      /// <summary>
      /// Общий итератор
      /// </summary>
      /// <typeparam name="T">Целевой тип объекта для списка</typeparam>
      /// <param name="aList">Объект рекурсивного списка</param>
      /// <param name="finalState">Делегат для факта достижения курсором конца списка</param>
      /// <param name="incrementer">Делегат для увеличения значения курсора</param>
      /// <returns>Общий итератор</returns>
      public static IEnumerable<T> GeneralIterator<T>(this IRecursiveList<T> aList,
         Func<IRecursiveList<T>, bool> finalState, Func<IRecursiveList<T>, IRecursiveList<T>> incrementer)
      {
         while (!finalState(aList))
         {
            yield return aList.Head;
            aList = incrementer(aList);
         }
      }

      /// <summary>
      /// Обратный итератор, 1-ая итерация
      /// </summary>
      /// <typeparam name="T">Целевой тип объекта для списка</typeparam>
      /// <param name="aList">Рекурсивный список</param>
      /// <returns>Обратный итератор</returns>
      public static IRecursiveList<T> Reverse1<T>(this IRecursiveList<T> aList)
      {
         var reverseList = new List<T>();
         Func<IRecursiveList<T>, List<T>> reverseFunc = null;
         reverseFunc = list =>
         {
            if (list != null)
            {
               reverseFunc(list.Tail);
               if (list.Tail != null)
               {
                  reverseList.Add(list.Head);
               }
            }
            return reverseList;
         };
         return MyList<T>.CreateList(reverseFunc(aList));
      }

      /// <summary>
      /// Обратный итератор, 2-ая итерация
      /// </summary>
      /// <typeparam name="T">Целевой тип объекта для списка</typeparam>
      /// <param name="aList">Рекурсивный список</param>
      /// <returns>Обратный итератор</returns>
      public static IRecursiveList<T> Reverse2<T>(this IRecursiveList<T> aList)
      {
         var reverseList = new MyList<T>(default(T), null);
         Func<IRecursiveList<T>, MyList<T>> reverseFunc = null;
         reverseFunc = list =>
         {
            if (list.Tail != null)
            {
               reverseList = new MyList<T>(list.Head, reverseList);
               reverseFunc(list.Tail);
            }
            return reverseList;
         };
         return reverseFunc(aList);
      }

      /// <summary>
      /// Обратный итератор, 3-ая итерация
      /// </summary>
      /// <typeparam name="T">Целевой тип объекта для списка</typeparam>
      /// <param name="theList">Рекурсивный список</param>
      /// <returns>Обратный итератор</returns>
      public static IRecursiveList<T> Reverse3<T>(this IRecursiveList<T> theList)
      {
         Func<IRecursiveList<T>, IRecursiveList<T>, IRecursiveList<T>> reverseFunc = null;
         reverseFunc =
            (list, result) => list.Tail != null
               ? reverseFunc(list.Tail, new MyList<T>(list.Head, result))
               : result;
         return reverseFunc(theList, new MyList<T>(default(T), null));
      }
   }
}