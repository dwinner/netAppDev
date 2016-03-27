/**
 * Облегченные опосредованные объекты
 */

using System;
using System.Collections.Generic;

namespace Flyweight
{
   internal static class Program
   {
      static void Main()
      {
         var audioEntities = new List<AudioEntity>
         {
            new AudioEntity{Album = "A", TrackName = "A"},
            new AudioEntity{Album = "B", TrackName = "B"},
            new AudioEntity{Album = "T", TrackName = "C"},
            new AudioEntity{Album = "C", TrackName = "D"},
            new AudioEntity{Album = "D", TrackName = "Z"},
            new AudioEntity{Album = "E", TrackName = "X"},
            new AudioEntity{Album = "F", TrackName = "F"}
         };

         Console.WriteLine("Raw Order:");
         audioEntities.ForEach(Console.WriteLine);
         Console.WriteLine();

         Console.WriteLine("Album order:");
         audioEntities.Sort(AudioComparerFlyweight.Instance[AudioComparisonType.Album]);
         audioEntities.ForEach(Console.WriteLine);
         Console.WriteLine();

         Console.WriteLine("Track Order:");
         audioEntities.Sort(AudioComparerFlyweight.Instance[AudioComparisonType.TrackName]);
         audioEntities.ForEach(Console.WriteLine);
      }
   }
}
