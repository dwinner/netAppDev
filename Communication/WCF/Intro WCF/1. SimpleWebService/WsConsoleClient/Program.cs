/*
 * Потребление Веб-службы.
 */
using System;

namespace WsConsoleClient
{
   public static class Program
   {
      static void Main()
      {
         SimpleWebServive webServive = new SimpleWebServive();
         string wsResult = webServive.SqrtWebMethod(9);
         Console.WriteLine(wsResult);
         Console.ReadKey();
      }
   }
}
