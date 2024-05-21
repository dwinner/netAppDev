using System;

namespace _13_Currying
{
   public static class CurryExtensions
   {
      public static Func<TArg1, TResult> Bind2Nd<TArg1, TArg2, TResult>(this Func<TArg1, TArg2, TResult> self,
         TArg2 arg2)
      {
         return arg1 => self(arg1, arg2);
      }
   }
}