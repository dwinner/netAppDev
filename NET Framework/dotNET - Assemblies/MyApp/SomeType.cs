namespace MyApp
{
   public class SomeType
   {
      public static int Factorial(in int n)
      {
         var result = 1;
         for (var i = 1; i <= n; i++)
         {
            result *= i;
         }

         return result;
      }
   }
}