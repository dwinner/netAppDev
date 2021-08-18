/**
 * Ковариантность:
 * Если T преобразуем в R, то IOperation<T> можно безопасно преобразовать в IOperation<R>
 */

using System;
using System.Collections.Generic;

namespace _07_Covariance
{
   static class Program
   {
      static void Main()
      {
         var strings = new MyCollection<string>();
         strings.AddItem("One");
         strings.AddItem("Two");
         IMyEnumerator<string> collStrings = strings;
         IMyEnumerator<object> collObjects = collStrings;   // Ковариантность в действии
         PrintCollection(collObjects, 2);

         Console.ReadKey();
      }

      static void PrintCollection(IMyEnumerator<object> coll, int count)
      {
         for (int i = 0; i < count; i++)
         {
            Console.WriteLine(coll.GetItem(i));
         }
      }
   }

   interface IMyCollection<in T>
   {
      void AddItem(T item);      
   }

   interface IMyEnumerator<out T>
   {
      T GetItem(int index);
   }

   class MyCollection<T> : IMyCollection<T>, IMyEnumerator<T>
   {
      private readonly IList<T> _collection = new List<T>();

      public T GetItem(int index)
      {
         return _collection[index];
      }

      public void AddItem(T item)
      {
         _collection.Add(item);
      }
   }
}
