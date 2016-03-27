/**
 * Контравариантность:
 * Если R преобразуем в T, то IOperation<T> можно безопасно преобразовать IOperation<R>
 */

using System;
using System.Collections.Generic;

namespace _08_Contravariance
{
   class Program
   {
      static void Main()
      {
         var items = new MyCollection<A>();
         items.AddItem(new A());
         var b = new B();
         items.AddItem(b);
         IMyTrimmableCollection<A> collItems = items;

         // Контравариантность в действии
         IMyTrimmableCollection<B> trimColl = collItems;
         trimColl.RemoveItem(b);

         Console.ReadKey();
      }
   }

   class A { }

   class B : A { }

   interface IMyCollection<in T>
   {
      void AddItem(T item);
   }

   interface IMyTrimmableCollection<in T>
   {
      void RemoveItem(T item);
   }

   class MyCollection<T> : IMyCollection<T>, IMyTrimmableCollection<T>
   {
      private readonly List<T> _collection = new List<T>();

      public void AddItem(T item)
      {
         _collection.Add(item);
      }

      public void RemoveItem(T item)
      {
         _collection.Remove(item);
      }
   }
}
