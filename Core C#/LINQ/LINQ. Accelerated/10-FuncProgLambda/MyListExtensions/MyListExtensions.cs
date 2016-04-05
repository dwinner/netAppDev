using System;
using System.Collections.Generic;

namespace _10_FuncProgLambda.MyListExtensions
{
   /// <summary>
   /// Методы расширения для стандартных операций запросов для рекурсивного списка
   /// </summary>
   public static class MyListExtensions
   {
      /// <summary>
      /// Прямой итератор
      /// </summary>
      /// <typeparam name="T">Параметр типа списка</typeparam>
      /// <param name="theList">Исходный список</param>
      /// <param name="finalState">Делегат, сигнализирующий об окончании итерации</param>
      /// <param name="incrementer">Делегат, выполняющий инкрементирование последовательности</param>
      /// <returns>Прямой перечислитель элементов</returns>
      public static IEnumerable<T> GeneralIterator<T>(this IMyList<T> theList,
         Func<IMyList<T>, bool> finalState,
         Func<IMyList<T>, IMyList<T>> incrementer)
      {
         while (!finalState(theList))
         {
            yield return theList.Head;
            theList = incrementer(theList);
         }
      }

      public static IMyList<T> Where<T>(this IMyList<T> theList, Func<T, bool> predicate)
      {
         Func<IMyList<T>> whereTailFunc = null;
         whereTailFunc = () =>
            {
               IMyList<T> result = null;
               if (theList.Tail == null)
                  result = new MyList<T>(default(T), null);
               if (predicate(theList.Head))
                  result = new MyList<T>(theList.Head, whereTailFunc);
               theList = theList.Tail;
               if (result == null)
                  result = whereTailFunc();
               return result;
            };
         return whereTailFunc();
      }

      public static IMyList<R> Select<T, R>(this IMyList<T> theList, Func<T, R> selector)
      {
         Func<IMyList<R>> selectorTailFunc = null;
         selectorTailFunc = () =>
            {
               IMyList<R> result = null;
               if (theList.Tail == null)
                  result = new MyList<R>(default(R), null);
               else
                  result = new MyList<R>(selector(theList.Head), selectorTailFunc);
               theList = theList.Tail;
               return result;
            };
         return selectorTailFunc();
      }

      public static IMyList<T> Take<T>(this IMyList<T> theList, int takeCount)
      {
         Func<IMyList<T>> takeTailFunc = null;
         takeTailFunc = () =>
            {
               IMyList<T> result = null;
               if (theList.Tail == null || takeCount-- == 0)
                  result = new MyList<T>(default(T), null);
               else
                  result = new MyList<T>(theList.Head, takeTailFunc);
               theList = theList.Tail;
               return result;
            };
         return takeTailFunc();
      }
   }
}
