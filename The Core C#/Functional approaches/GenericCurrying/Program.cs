/**
 * Обобщенное каррирование
 */

using System;

namespace _10_GenericCarrying
{
   class Program
   {
      static void Main()
      {
         var binder = new Bind2Nd<int, int, int>(Add, 4);
         Console.WriteLine(binder.Binder(2));

         Console.ReadKey();
      }

      static int Add(int x, int y)
      {
         return x + y;
      }
   }

   public class Bind2Nd<TArg1Type, TArg2Type, TReturnType>
   {
      private readonly Func<TArg1Type, TArg2Type, TReturnType> _del;
      private readonly TArg2Type _arg2;

      public Bind2Nd(Func<TArg1Type, TArg2Type, TReturnType> del, TArg2Type arg2)
      {
         _del = del;
         _arg2 = arg2;
      }

      public Func<TArg1Type, TReturnType> Binder
      {
         get
         {
            return arg1 => _del(arg1, _arg2);
         }
      }
   }
}
