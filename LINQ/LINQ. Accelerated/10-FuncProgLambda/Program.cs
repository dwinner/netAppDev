/**
 * Приемы функционального программирования
 */

using System;
using _10_FuncProgLambda.MyListExtensions;

namespace _10_FuncProgLambda
{
   class Program
   {
      static void Main()
      {
         var infiniteList = CreateInfiniteList(21);
         var linkList = infiniteList.Where(x => x > 3).Select(x => x * 2).Take(10);
         var iterator = linkList.GeneralIterator(list => list.Tail == null, list => list.Tail);
         foreach (var item in iterator)
         {
            Console.Write("{0}, ", item);
         }

         Console.WriteLine();
         Console.ReadLine();
      }

      static IMyList<T> CreateInfiniteList<T>(T item)
      {
         Func<IMyList<T>> tailGenerator = null;
         tailGenerator = () => new MyList<T>(item, tailGenerator);
         return tailGenerator();
      }
   }
}
