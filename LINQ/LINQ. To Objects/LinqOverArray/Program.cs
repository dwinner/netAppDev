using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOverArray
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with LINQ to Objects *****\n");
         QueryOverStrings();
         Console.WriteLine();
         QueryOverStringsLongHand();
         Console.WriteLine();
         QueryOverInts();
         Console.WriteLine();

         Console.ReadLine();
      }

      #region Запрос Linq на массиве строк
      static void QueryOverStrings()
      {         
         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter", "System Shock 2"
         };
         
         IEnumerable<string> subset = from g in currentVideoGames
                                      where g.Contains(" ")
                                      orderby g
                                      select g;


         ReflectOverQueryResults(subset);
         
         foreach (string s in subset)
            Console.WriteLine("Item: {0}", s);
      }
      #endregion

      static void ReflectOverQueryResults(object resultSet)
      {
         Console.WriteLine("***** Info about your query *****");
         Console.WriteLine("resultSet is of type: {0}",
            resultSet.GetType().Name);
         Console.WriteLine("resultSet location: {0}",
           resultSet.GetType().Assembly.GetName().Name);
      }

      #region Запрос Linq к массиву Int32. Отложенное выполнение.
      static void QueryOverInts()
      {
         int[] numbers = { 10, 20, 30, 40, 1, 2, 3, 8 };

         // Получить числа, которые меньше, чем 10.
         var subset = from i in numbers where i < 10 select i;

         // LINQ оператор вычислится здесь
         foreach (var i in subset)
            Console.WriteLine("{0} < 10", i);
         Console.WriteLine();

         // Меняем данные.
         numbers[0] = 4;

         // Вычисляем снова
         foreach (var j in subset)
            Console.WriteLine("{0} < 10", j);

         Console.WriteLine();
         ReflectOverQueryResults(subset);
      }
      #endregion

      #region Эквивалентное решение без использовыания LINQ
      static void QueryOverStringsLongHand()
      {         
         string[] currentVideoGames =
         {
            "Morrowind", "Uncharted 2",
            "Fallout 3", "Daxter",
            "System Shock 2"
         };

         string[] gamesWithSpaces = new string[5];

         for (int i = 0; i < currentVideoGames.Length; i++)
         {
            if (currentVideoGames[i].Contains(" "))
               gamesWithSpaces[i] = currentVideoGames[i];
         }
         
         Array.Sort(gamesWithSpaces);
         
         foreach (string s in gamesWithSpaces)
         {
            if (s != null)
               Console.WriteLine("Item: {0}", s);
         }
         Console.WriteLine();
      }
      #endregion

      #region Немедленное выполнение запроса Linq
      static void ImmediateExecution()
      {
         int[] numbers = { 10, 20, 30, 40, 1, 2, 3, 8 };

         // Выполнить запрос сразу и получить int[].
         int[] subsetAsIntArray =
           (from i in numbers where i < 10 select i).ToArray<int>();

         // Выполнить запрос сразу и получить List<int>.
         List<int> subsetAsListOfInts =
            (from i in numbers where i < 10 select i).ToList<int>();         
      }
      #endregion
   }
}
