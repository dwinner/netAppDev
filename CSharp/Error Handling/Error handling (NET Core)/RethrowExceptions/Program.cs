using System;

internal class Program
{
   private static void Main()
   {
      HandleAll();
      Console.ReadLine();
   }

   public static void HandleAll()
   {
      Action[] methods =
      {
         HandleAndThrowAgain,
         HandleAndThrowWithInnerException,
         HandleAndRethrow,
         HandleWithFilter
      };

      foreach (var m in methods)
      {
         try
         {
            m(); // line 114
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
               Console.WriteLine($"\tInner Exception {ex.InnerException.Message}");
               Console.WriteLine(ex.InnerException.StackTrace);
            }

            Console.WriteLine();
         }
      }
   }

   public static void HandleWithFilter()
   {
      try
      {
         ThrowAnException("test 4"); // line 1004
      }
      catch (Exception ex) when (Filter(ex))
      {
         Console.WriteLine("block never invoked");
      }
   }

   public static bool Filter(Exception ex)
   {
      Console.WriteLine($"just log {ex.Message}");
      return false;
   }

   public static void HandleAndRethrow()
   {
      try
      {
         ThrowAnException("test 3");
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Log exception {ex.Message} and rethrow");
         throw; // line 2009
      }
   }

   public static void HandleAndThrowWithInnerException()
   {
      try
      {
         ThrowAnException("test 2");
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Log exception {ex.Message} and throw again");
         throw new AnotherCustomException("throw with inner exception", ex); // line 3009
      }
   }

   public static void HandleAndThrowAgain()
   {
      try
      {
         ThrowAnException("test 1");
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Log exception {ex.Message} and throw again");
         throw ex; // you shouldn't do that - line 4009
      }
   }

   public static void ThrowAnException(string message)
   {
      throw new MyCustomException(message); // line 8002
   }
}