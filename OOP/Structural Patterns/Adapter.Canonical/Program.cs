/**
 * Канонические формы Адаптеров
 */

using System;

namespace Adapter.Canonical
{
   internal static class Program
   {
      private static void Main()
      {
         var target = new Target();
         target.Request();

         var objectAdapter = new ObjectAdapter();
         objectAdapter.Request();

         Console.Read();
      }
   }
}