/**
 * Инкапсуляция команд
 */

using System;

namespace Command
{
   internal static class Program
   {
      private static void Main()
      {
         // Creating the user
         var user = new User();

         // Let the user do something
         user.Compute('+', 100);
         user.Compute('-', 50);
         user.Compute('*', 10);
         user.Compute('/', 2);

         // Undo 4 last commands
         user.Undo(4);

         // Redo 3 cancelled commands
         user.Redo(3);

         Console.ReadKey();
      }
   }
}