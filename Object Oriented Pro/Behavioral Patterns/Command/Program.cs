/**
 * Инкапсуляция команд
 */

using System;

namespace Command
{
   static class Program
   {
      static void Main()
      {
         // Создаем пользователя
         var user = new User();

         // Пусть он что-нибудь сделает
         user.Compute('+', 100);
         user.Compute('-', 50);
         user.Compute('*', 10);
         user.Compute('/', 2);

         // Отменяем 4 команды
         user.Undo(4);

         // Вернем 3 отмененные команды
         user.Redo(3);

         Console.ReadKey();
      }
   }
}
