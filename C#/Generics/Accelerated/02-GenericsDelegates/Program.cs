/**
 * Обобщенные делегаты
 */ 

using System;
using System.Collections.Generic;

namespace _02_GenericsDelegates
{
   class Program
   {
      static void Main()
      {
         var delegates = new DelegateContainer<int>();
         delegates.Add(PrintInt);
         delegates.CallDelegates(42);

         Console.ReadKey();
      }

      static void PrintInt(int i)
      {
         Console.WriteLine(i);
      }
   }

   public delegate void MyDelegate<in T>(T i);

   public class DelegateContainer<T>
   {
      private readonly IList<MyDelegate<T>> _imp = new List<MyDelegate<T>>();

      public void Add(MyDelegate<T> del)
      {
         _imp.Add(del);
      }

      public void CallDelegates(T k)
      {
         foreach (MyDelegate<T> del in _imp)
         {
            del(k);
         }
      }
   }
}
