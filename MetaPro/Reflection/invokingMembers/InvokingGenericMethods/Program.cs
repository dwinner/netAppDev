namespace InvokingGenericMethods;

internal class Program
{
   private static void Main(string[] args)
   {
      var echo = typeof(Program).GetMethod("Echo");
      Console.WriteLine(echo.IsGenericMethodDefinition); // True

      try
      {
         echo.Invoke(null, new object[] { 123 }); // Exception
      }
      catch (Exception ex)
      {
         Console.WriteLine(ex.Message);
      }

      var intEcho = echo.MakeGenericMethod(typeof(int));
      Console.WriteLine(intEcho.IsGenericMethodDefinition); // False
      Console.WriteLine(intEcho.Invoke(null, new object[] { 3 })); // 3
   }

   public static T Echo<T>(T x) => x;
}