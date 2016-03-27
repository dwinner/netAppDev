using System;
using System.Threading;
using MathClient.MathServiceReference;

namespace MathClient
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("----- The async math client -----\n");

         using (BasicMathClient mathClient = new BasicMathClient())
         {
            mathClient.Open();
            IAsyncResult result = mathClient.BeginAdd(2, 3, ar =>
               {                  
                  Console.WriteLine("2 + 3 = {0}", mathClient.EndAdd(ar));
               }, null);
            while (!result.IsCompleted)
            {
               Thread.Sleep(200);
               Console.WriteLine("Client working...");
            }
         }

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }
   }
}
