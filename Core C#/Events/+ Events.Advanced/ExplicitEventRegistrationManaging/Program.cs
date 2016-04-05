/**
 * Явное управление регистрацией событий
 */

using System;

namespace ExplicitEventRegistrationManaging
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         var events = new TypeOfLotsOfEvents();
         events.Foo += events_Foo;
         events.SimulateFoo();
      }

      private static void events_Foo(object sender, FooEventArgs e)
      {
         Console.WriteLine("Handling Foo Event here...");
      }
   }
}