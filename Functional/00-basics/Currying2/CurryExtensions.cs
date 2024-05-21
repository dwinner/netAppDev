using System;

namespace _14_Currying2
{
   public static class CurryExtensions
   {
      public static Func<TArg2, Func<TArg1, TResult>> Bind2Nd<TArg1, TArg2, TResult>(
         this Func<TArg1, TArg2, TResult> self)
      {
         return arg2 => arg1 => self(arg1, arg2);
      }
   }
}