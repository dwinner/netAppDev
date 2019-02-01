/**
 * Демонстрация наиболее эффективных вариантов разработки событий
 */

using System;

namespace EventAreas
{
   internal static class Program
   {
      private static void Main()
      {
         var mailManager = new MailManager();
         mailManager.NewMail += (sender, eventArgs) => { Console.WriteLine(eventArgs); };
         mailManager.SimulateNewMail("me", "me", "test");
      }
   }
}