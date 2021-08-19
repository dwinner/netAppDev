using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharp8Features
{
   internal static class Program
   {
      private static async Task Main()
      {
         _ = new Demo();
         await MainAsync();
         _ = Console.ReadLine();
      }


      #region asynchronous streams

      private static async Task MainAsync()
      {
         foreach (var item in await GetSomethingAsync1())
         {
            Console.WriteLine(item);
         }


         await foreach (var item in GetSomethingAsync2())
         {
            Console.WriteLine(item);
         }

         _ = Console.ReadLine();
      }


      private static async Task<IEnumerable<int>> GetSomethingAsync1()
      {
         var iValues = new List<int>();
         for (var i = 0; i <= 10; i++)
         {
            await Task.Delay(1000);
            iValues.Add(i);
         }

         return iValues;
      }

      private static async IAsyncEnumerable<int> GetSomethingAsync2()
      {
         for (var i = 0; i <= 10; i++)
         {
            yield return i;
            await Task.Delay(1000);
         }
      }

      #endregion
   }
}