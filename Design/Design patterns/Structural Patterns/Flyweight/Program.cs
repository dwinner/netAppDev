/**
 * Облегченные опосредованные объекты
 */

using System.Collections.Generic;
using static System.Console;

namespace Flyweight
{
   internal static class Program
   {
      private static void Main()
      {
         var audioEntities = new List<AudioEntity>
         {
            new AudioEntity {Album = "A", TrackName = "A"},
            new AudioEntity {Album = "B", TrackName = "B"},
            new AudioEntity {Album = "T", TrackName = "C"},
            new AudioEntity {Album = "C", TrackName = "D"},
            new AudioEntity {Album = "D", TrackName = "Z"},
            new AudioEntity {Album = "E", TrackName = "X"},
            new AudioEntity {Album = "F", TrackName = "F"}
         };

         WriteLine("Raw Order:");
         audioEntities.ForEach(WriteLine);
         WriteLine();

         WriteLine("Album order:");
         audioEntities.Sort(AudioComparerFlyweight.Instance[AudioComparisonType.Album]);
         audioEntities.ForEach(WriteLine);
         WriteLine();

         WriteLine("Track Order:");
         audioEntities.Sort(AudioComparerFlyweight.Instance[AudioComparisonType.TrackName]);
         audioEntities.ForEach(WriteLine);
      }
   }
}