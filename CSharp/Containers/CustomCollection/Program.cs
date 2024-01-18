/*
 * Создание собственных коллекций
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _08_CustomCollection
{
   class Program
   {
      static void Main()
      {
         var collection = new MyCollection<int>(GenerateNumbers());
         foreach (int n in collection)
         {
            Console.WriteLine(n);
         }

         Console.ReadKey();
      }

      static IEnumerable<int> GenerateNumbers()
      {
         for (int i = 4; i >= 0; --i)
         {
            yield return i;
         }
      }
   }

   public class MyCollection<T> : Collection<T>
   {
      public MyCollection()
      { }

      public MyCollection(IList<T> list)
         : base(list)
      { }

      public MyCollection(IEnumerable<T> enumerable)
      {
         foreach (T item in enumerable)
         {
            Add(item);
         }
      }

      public MyCollection(IEnumerator<T> enumerator)
      {
         while (enumerator.MoveNext())
         {
            Add(enumerator.Current);
         }
      }
   }
}
