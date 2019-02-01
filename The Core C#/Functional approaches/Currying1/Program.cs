/**
 * Каррирование
 */

using System;

namespace _09_Carrying
{
   class Program
   {
      static void Main(string[] args)
      {
         Bind2dn binder = new Bind2dn(new Operation(Add), 4);
         Console.WriteLine(binder.Binder(2));

         Console.ReadKey();
      }

      static int Add(int x, int y)
      {
         return x + y;
      }
   }

   public delegate int Operation(int x, int y);

   public class Bind2dn
   {
      private Operation del;
      private int arg2;

      public delegate int BoundDelegate(int x);

      public Bind2dn(Operation del, int arg2)
      {
         this.del = del;
         this.arg2 = arg2;
      }

      public BoundDelegate Binder
      {
         get
         {
            return delegate(int arg1)
            {
               return del(arg1, arg2);
            };
         }
      }
   }
}
