/**
 * Выражения значений по умолчанию
 */

using System;

namespace _03_DefaultValues
{
   class Program
   {
      static void Main()
      {
         var intColl = new MyContainer<int>();
         var objColl = new MyContainer<object>();
         Console.WriteLine(intColl.IsDefault(0));
         Console.WriteLine(objColl.IsDefault(0));

         Console.ReadKey();
      }
   }

   public class MyContainer<T>
   {
      private readonly T[] _imp;

      public MyContainer()
      {
         // Начальное наполнение
         _imp = new T[4];
         for (int i = 0; i < _imp.Length; i++)
         {
            _imp[i] = default(T);
         }
      }

      public bool IsDefault(int index)
      {
         if (index < 0 || index >= _imp.Length)
            throw new ArgumentOutOfRangeException();
         return Equals(_imp[index], default(T));
      }
   }
}
