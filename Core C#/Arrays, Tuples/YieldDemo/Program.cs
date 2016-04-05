/**
 * Реализация перечислителей (итераторов) в C# 2.0+
 */

using System;
using System.Collections;

namespace YieldDemo
{
   static class Program
   {
      static void Main()
      {
         // Конечный автомат на базе yield
         var collection = new YieldCollection();
         foreach (var str in collection)
         {
            Console.WriteLine(str);
         }

         // Различные реализации итераторов
         var musicTitles = new MusicTitles();
         foreach (var title in musicTitles)
         {
            Console.WriteLine(title);
         }
         Console.WriteLine();
         Console.WriteLine("Reverse");
         foreach (var musicTitle in musicTitles.Reverse())
         {
            Console.WriteLine(musicTitle);
         }
         Console.WriteLine();
         Console.WriteLine("Subset");
         foreach (var title in musicTitles.Subset(2, 2))
         {
            Console.WriteLine(title);
         }

         // Более сложные элементы итераторов
         var game = new GameMoves();
         IEnumerator enumerator = game.Cross();
         while (enumerator != null && enumerator.MoveNext())
         {
            enumerator = enumerator.Current as IEnumerator;
         }
      }
   }
}
