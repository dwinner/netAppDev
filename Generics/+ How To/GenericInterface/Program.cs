/**
 * Параметризованные интерфейсы
 */

using System;

namespace HowToCSharp.ch09.GenericInterface
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("No output for this project. Just source code example");
         Console.ReadKey();
      }

      interface IUnique<out T>
      {
         T Id { get; }
      }

      class MyObject : IUnique<int>
      {
         private int _id;

         public int Id
         {
            get { return _id; }
         }
      }

      class MyOtherObject : IUnique<string>
      {
         private string _id;

         public string Id
         {
            get { return _id; }
         }
      }
   }
}
