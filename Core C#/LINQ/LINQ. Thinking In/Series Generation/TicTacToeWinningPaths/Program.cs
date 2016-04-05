// Построение выигрышных марштуров в игре TicTacToe

using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeWinningPaths
{
   internal static class Program
   {
      private const int BoardSize = 3;

      private static void Main()
      {
         var range = Enumerable.Range(1, BoardSize * BoardSize).ToArray();
         var winningPaths = new List<List<int>>();
         Enumerable.Range(0, BoardSize)
            .ToList()
            .ForEach(k => winningPaths.Add(range.Skip(k * BoardSize).Take(BoardSize).ToList()));
         
         // Горизонтальные маршруты
         Enumerable.Range(0, BoardSize)
            .ToList()
            .ForEach(k => winningPaths.Add(winningPaths.Take(BoardSize).Select(p => p[k]).ToList()));
         
         // Вертикальные маршруты
         winningPaths.Add(range.Where((r, i) => i % (BoardSize + 1) == 0).ToList());

         // Главная диагональ
         winningPaths.Add(range.Where((r, i) => i % (BoardSize - 1) == 0).Skip(1).Take(BoardSize).ToList());
         
         // Побочная диагональ
         var dump =
            winningPaths.Select(
               x =>
                  x.Select(z => z.ToString()).Aggregate((a, b) => string.Format("{0} {1}", a.ToString(), b.ToString())));

         Console.WriteLine(dump.Aggregate(string.Empty,
            (accumulator, current) => accumulator + string.Format("{0}{1}", current, Environment.NewLine)));
      }
   }
}