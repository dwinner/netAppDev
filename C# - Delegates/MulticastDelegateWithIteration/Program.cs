/**
 * Список вызова делегата
 */

using System;

namespace MulticastDelegateWithIteration
{
   static class Program
   {
      static void Main()
      {
         Action outputAction = One;
         outputAction += Two;

         Delegate[] invocationList = outputAction.GetInvocationList();
         foreach (Action @delegate in invocationList)
         {
            try
            {
               @delegate();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
         }

         Console.ReadKey();
      }

      static void One()
      {
         Console.WriteLine("One");
         throw new Exception("Error in one");
      }

      static void Two()
      {
         Console.WriteLine("Two");
      }
   }
}
