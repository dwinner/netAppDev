namespace TypeExtensions;

public static class Operators
{
   extension<T>(T self) where T : class?
   {
      public void operator >>= (Action<T> op) => op(self);
   }

   /*extension<T>(ref T self) where T: struct{

      public static void operator >>= (Action<T> op) => op(self);
   }*/

   extension<TIn, TOut>(TIn)
   {
      public static TOut operator |(TIn self, Func<TIn, TOut> op) => op(self);
   }
}