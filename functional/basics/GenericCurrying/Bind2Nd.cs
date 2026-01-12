using System;

namespace _10_GenericCarrying
{
   public class Bind2Nd<TArg1, TArg2, TReturn>
   {
      private readonly TArg2 _arg2;
      private readonly Func<TArg1, TArg2, TReturn> _del;

      public Bind2Nd(Func<TArg1, TArg2, TReturn> del, TArg2 arg2)
      {
         _del = del;
         _arg2 = arg2;
      }

      public Func<TArg1, TReturn> Binder
      {
         get { return arg1 => _del(arg1, _arg2); }
      }
   }
}