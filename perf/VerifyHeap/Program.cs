using System;

namespace VerifyHeap
{
   internal static class Program
   {
      private static void Main()
      {
         const string heapVerifyVariable = "COMPLUS_HeapVerify";
         var heapVerify = Environment.GetEnvironmentVariable(heapVerifyVariable);
         Console.WriteLine($"{heapVerifyVariable}={heapVerify}");

         while (true)
         {
            const int allocAmount = 100;
            Console.Write($"AllocAmount = {allocAmount}, WriteAmount=");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var writeAmount))
            {
               var buffer = new int[allocAmount];
               unsafe
               {
                  fixed (int* beginning = buffer)
                  {
                     var p = beginning;
                     for (var i = 0; i < writeAmount; i++)
                     {
                        *p = i;
                        p++;
                     }
                  }
               }

               GC.Collect(2);
            }
            else
            {
               Console.WriteLine("Error");
            }
         }
      }
   }
}