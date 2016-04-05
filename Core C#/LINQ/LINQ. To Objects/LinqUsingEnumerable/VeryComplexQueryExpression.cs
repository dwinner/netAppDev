using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqUsingEnumerable
{
   class VeryComplexQueryExpression
   {
      public static void QueryStringsWithRawDelegates()
      {
         Console.WriteLine("***** Using Raw Delegates *****");

         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter",
            "System Shock 2"
         };

         // Генерируем необходимые делегаты Func<>.
         Func<string, bool> searchFilter = new Func<string, bool>(Filter);
         Func<string, string> itemToProcess = new Func<string, string>(ProcessItem);

         // Передаем делегаты в расширяющие методы Enumerable.
         var subset = currentVideoGames
           .Where(searchFilter).OrderBy(itemToProcess).Select(itemToProcess);

         // Печатаем результаты.
         foreach (var game in subset)
            Console.WriteLine("Item: {0}", game);
         Console.WriteLine();
      }

      // Назначения делегатов.
      public static bool Filter(string game)
      {
         return game.Contains(" ");
      }
      
      public static string ProcessItem(string game)
      {
         return game;
      }
   }
}
