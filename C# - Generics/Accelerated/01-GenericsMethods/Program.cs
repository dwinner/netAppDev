/**
 * Обобщенные методы
 */ 

using System;
using System.Collections.Generic;

namespace _01_GenericsMethods
{
   class Program
   {
      static void Main()
      {
         var lContainer = new MyContainer<long>();
         var iContainer = new MyContainer<int>();
         lContainer.Add(1);
         lContainer.Add(2);
         lContainer.Add(3);
         lContainer.Add(4);
         lContainer.Add(iContainer, IntToLongConverter);
         foreach (long l in lContainer)
         {
            Console.WriteLine(l);
         }

         Console.ReadKey();
      }

      static long IntToLongConverter(int i)
      {
         return i;
      }
   }

   public class MyContainer<T> : IEnumerable<T>
   {
      private readonly IList<T> _impl = new List<T>();

      public void Add(T item)
      {
         _impl.Add(item);
      }

      /*
       * Converter<TInput, TOutput> - новый тип делегата, введенный
       * в .NET Framework 2.0, который может быть привязан к методу,
       * "знающему", как преобразовывать тип TInput в тип TOutput
       */
      
      public void Add<TR>(MyContainer<TR> otherContainer, Converter<TR, T> converter)
      {
         foreach (TR item in otherContainer)
         {
            _impl.Add(converter(item));
         }
      }

      public IEnumerator<T> GetEnumerator()
      {
         return _impl.GetEnumerator();
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}
