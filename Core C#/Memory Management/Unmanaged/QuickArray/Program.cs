/**
 * Выделение памяти для массивов в стеке
 */

using System;

namespace QuickArray
{
   internal static class Program
   {
      private static unsafe void Main()
      {
         Console.Write("How big an array do you want? \n> ");
         string userInput = Console.ReadLine();
         if (userInput == null)
         {
            return;
         }

         uint size = uint.Parse(userInput);

         long* pArray = stackalloc long[(int)size];
         for (int i = 0; i < size; i++)   // Note: Привычный дл C# доступ к массиву
         {
            pArray[i] = i * i;
         }

         for (int i = 0; i < size; i++)   // Note: Доступ к массиву через указатели
         {
            Console.WriteLine("Element {0} = {1}", i, *(pArray + i));
         }

         Console.ReadLine();
      }
   }
}
