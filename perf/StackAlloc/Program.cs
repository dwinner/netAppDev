using System;

namespace StackAlloc
{
   internal static class Program
   {
      private static void Main()
      {
         while (true)
         {
            Console.Write("Enter size to stackalloc ('q' to exit): ");
            var input = Console.ReadLine();
            if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
               break;
            }

            if (int.TryParse(input, out var size))
            {
               try
               {
                  DoStackAlloc(size);

                  Console.WriteLine($"Allocated {size}-size array");
               }
               catch (StackOverflowException ex)
               {
                  Console.WriteLine("Caught SOE!");
               }
            }
         }
      }

      private static unsafe void DoStackAlloc(int size)
      {
         var buffer = stackalloc int[size];
         for (var i = 0; i < size; i++)
         {
            buffer[i] = i;
         }
      }
   }
}