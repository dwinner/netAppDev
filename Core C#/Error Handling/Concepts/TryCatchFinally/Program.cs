/**
 * Операторы try-catch-finally
 */

using System;
using System.Collections;

namespace _01_TryCatchFinally
{
   //[assembly: RuntimeCompatibility(WrapNonExceptionThrows = false)]
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            ArrayList list = new ArrayList();
            list.Add(1);
            Console.WriteLine("Элемент 10 = {0}", list[10]);
         }
         catch (ArgumentOutOfRangeException x)
         {
            Console.WriteLine(x);
         }
         /*catch (Exception x)
         {
            Console.WriteLine(x);
         }*/
         catch
         {
            Console.WriteLine("Unhandled exception");
            //RuntimeWrappedException rtEx = new RuntimeWrappedException();
            //rtEx.WrappedException;
         }
         finally
         {
            Console.WriteLine("Cleaning");
         }

         Console.ReadKey();
      }
   }
}
