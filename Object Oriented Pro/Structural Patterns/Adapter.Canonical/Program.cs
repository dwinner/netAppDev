/**
 * Канонические формы Адаптеров
 */

using System;

namespace Adapter.Canonical
{
   static class Program
   {
      static void Main()
      {
         var target = new Target();
         target.Request();
         Console.Read();
      }
   }
}
