using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqUsingEnumerable
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** LINQ: Simple to WAY Complex *****\n");

         QueryStringWithOperators();
         Console.WriteLine();
         QueryStringsWithEnumerableAndLambdas();
         Console.WriteLine();
         QueryStringsWithAnonymousMethods();
         Console.WriteLine();
         VeryComplexQueryExpression.QueryStringsWithRawDelegates();
         Console.ReadLine();
      }

      #region LINQ операторы
      static void QueryStringWithOperators()
      {
         Console.WriteLine("***** Using Query Operators *****");

         string[] currentVideoGames = {"Morrowind", "Uncharted 2",
                "Fallout 3", "Daxter", "System Shock 2"};

         var subset = from game in currentVideoGames
                      where game.Contains(" ")
                      orderby game
                      select game;

         foreach (string s in subset)
            Console.WriteLine("Item: {0}", s);
      }
      #endregion

      #region LINQ через лямбда-выражения и методы расширения Enumerable
      static void QueryStringsWithEnumerableAndLambdas()
      {
         Console.WriteLine("***** Using Enumerable / Lambda Expressions *****");

         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter",
            "System Shock 2"
         };
         
         // Строим выражение запроса, используя методы расширения и лямбда
         var subset = currentVideoGames.Where(game => game.Contains(" "))
           .OrderBy(game => game)
           .Select(game => game);

         // Печать результатов.
         foreach (var game in subset)
            Console.WriteLine("Item: {0}", game);
         Console.WriteLine();
      }

      static void QueryStringsWithEnumerableAndLambdas2()
      {
         Console.WriteLine("***** Using Enumerable / Lambda Expressions (version 2) *****");

         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter",
            "System Shock 2"
         };

         // Делим цепочку на отдельные вызовы
         var gamesWithSpaces = currentVideoGames.Where(game => game.Contains(" "));
         var orderedGames = gamesWithSpaces.OrderBy(game => game);
         var subset = orderedGames.Select(game => game);

         foreach (var game in subset)
            Console.WriteLine("Item: {0}", game);
         Console.WriteLine();
      }

      #endregion

      #region LINQ через использование анонимных методов
      static void QueryStringsWithAnonymousMethods()
      {
         Console.WriteLine("***** Using Anonymous Methods *****");

         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter",
            "System Shock 2"
         };

         // Генерируем необходимые делегаты Func<>, используя анонимные методы.
         Func<string, bool> searchFilter = delegate(string game)
         {
            return game.Contains(" ");
         };
         Func<string, string> itemToProcess = delegate(string s)
         {
            return s;
         };

         // Передаем делегаты в расширяющие методы Enumerable.
         var subset = currentVideoGames.Where(searchFilter)
           .OrderBy(itemToProcess)
           .Select(itemToProcess);

         // Печатаем результаты.
         foreach (var game in subset)
            Console.WriteLine("Item: {0}", game);
         Console.WriteLine();
      }
      #endregion
   }
}
