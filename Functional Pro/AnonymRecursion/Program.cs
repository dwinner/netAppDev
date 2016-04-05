/**
 * Анонимная рекурсия
 */


namespace _15_AnonymRecursion
{
   internal delegate TResult AnonRec<TArg, TResult>(AnonRec<TArg, TResult> f, TArg arg);

   static class Program
   {
      static void Main()
      {
         //Func<int, int> fact = null;
         //fact = x => x > 1 ? x * fact(x - 1) : 1;
         AnonRec<int, int> fact = (f, x) => x > 1 ? x * f(f, x - 1) : 1;
      }
   }
}
